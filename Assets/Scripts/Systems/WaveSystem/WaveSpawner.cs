using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Systems.AttributeSystem;
using Systems.GameSystem;
using Systems.MapSystem;
using Systems.NpcSystem;
using UnityEngine;

namespace Systems.WaveSystem
{
    class WaveSpawner : MonoBehaviour
    {
        private readonly float _npcSpawnInterval = WaveData.NpcSpawnInterval;
        public int NumberOfWaves { get; } = WaveData.WavePacks.Count;
        public int WaveCooldown { get; } = WaveData.WaveCooldown;


        public int CurrentWaveCount { get; private set; }

        public List<Wave> CurrentSpawnedWaves { get; private set; }

        public Wave CurrentSpawnedWave { get; private set; }

        public int CurrentElapsedTime { get; set; }

        private bool _waitingForWave;

        public WaveSpawner()
        {
            CurrentElapsedTime = 0;
        }

        //event stuff
        internal delegate void NpcHandler(Npc npc);
        public event NpcHandler OnNpcSpawned;

        private void OnEnable()
        {
            CurrentSpawnedWaves = new List<Wave>();
            CurrentElapsedTime = 0;
            CurrentWaveCount = 0;
            _waitingForWave = false;
        }

        private void Update()
        {
            if (!GameManager.Instance.PlayerReady) return;

            if (CurrentWaveCount >= NumberOfWaves && CurrentSpawnedWaves.Count == 0)
            {
                GameManager.Instance.WinGame();
            }

            if (CurrentSpawnedWaves.Count == 0 && !_waitingForWave)
            {
                _waitingForWave = true;
                StartCoroutine(WaitForNextWave());
            }

            var wavesToRemove = new List<Wave>();
            CurrentSpawnedWaves.ForEach(wave =>
            {
                
                if (wave.NpcSpawnCount >= wave.NpcCount && wave.SpawnedNpcs.All(npc => npc == null))
                {
                    //give rewards
                    GameManager.Instance.Player.IncreaseAmbassadors(GameSettings.AmbassadorsPerWave);

                    for (int i = 0; i < GameSettings.TowersPerWave; i++)
                    {
                        GameManager.Instance.TowerBuildManager.AddRandomBuildableTower();
                    }

                    wavesToRemove.Add(wave);
                }
            });

            CurrentSpawnedWaves = CurrentSpawnedWaves.Except(wavesToRemove).ToList();
        }

        private void StartSpawnWave()
        {
            if (CurrentWaveCount >= NumberOfWaves) return;

            _waitingForWave = false;
            CurrentWaveCount += 1;
            CurrentElapsedTime = WaveCooldown;

            var wave = GameManager.Instance.WaveGenerator.GenerateWave(CurrentWaveCount);
            wave.WaveNumber = CurrentWaveCount;
            CurrentSpawnedWave = wave;
            CurrentSpawnedWaves.Add(wave);

            Debug.Log("----- Spawning Wave " + CurrentWaveCount + "-----");
            StartCoroutine(SpawnWave(wave));
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            wave.NpcSpawnCount = 0;
            
            foreach (var pack in wave.Packs)
            {
                foreach (var npc in pack.Npcs)
                {
                    SpawnNpc(npc, wave, pack);
                    wave.NpcSpawnCount += 1;

                    yield return new WaitForSeconds(_npcSpawnInterval);
                }

                wave.PackSpawnCount += 1;

                yield return new WaitForSeconds(_npcSpawnInterval*2);
            }

            Debug.Log("----- Finished spawning Wave " + CurrentWaveCount + "-----");
            Debug.Log("----- Spawned " + wave.NpcSpawnCount + " npcs -----");
        }

        IEnumerator WaitForNextWave()
        {
            CurrentElapsedTime = 0;

            while (CurrentElapsedTime < WaveCooldown)
            {
                CurrentElapsedTime += 1;

                yield return new WaitForSeconds(1.0f);
            }

            StartSpawnWave();
        }

        void SpawnNpc(Npc npc, Wave wave, WavePack pack)
        {
            npc.IsSpawned = true;
            npc.InitData();
            
            npc.transform.parent = transform;
            npc.name = npc.Name + " (id: " + wave.PackCount + "/" + wave.NpcSpawnCount + ")";
            npc.transform.position = GameManager.Instance.MapManager.StartTile.GetTopCenter();

            npc.InitVisuals();

            npc.gameObject.SetActive(true);
            npc.SetLevel(CurrentWaveCount);

            wave.SpawnedNpcs.Add(npc);
            npc.SpawnedInWave = wave;

            npc.GetComponentInChildren<Collider>().gameObject.layer = LayerMask.NameToLayer("Npcs");

            var animator = npc.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.Rebind();
            }

            DebugPrintSpawnNpcData(npc);

            OnNpcSpawned?.Invoke(npc);
        }

        public void SpawnSingleNpcForCurrentWave(Npc npc, Vector3 position, Tile target)
        {
            var wave = CurrentSpawnedWave;

            npc.IsSpawned = true;
            npc.InitData();

            npc.transform.parent = transform;
            npc.name = npc.Name + " (id: " + wave.PackCount + "/" + wave.NpcSpawnCount + ")";
            npc.transform.position = position;
            npc.Target = target;

            npc.InitVisuals();

            npc.gameObject.SetActive(true);
            npc.SetLevel(CurrentWaveCount);

            wave.SpawnedNpcs.Add(npc);
            npc.SpawnedInWave = wave;
            npc.GetComponentInChildren<Collider>().gameObject.layer = LayerMask.NameToLayer("Npcs");

            var animator = npc.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.Rebind();
            }

            DebugPrintSpawnNpcData(npc);

            OnNpcSpawned?.Invoke(npc);
        }

        public List<Npc> GetCurrentSpawnedNpcs()
        {
            var npcs = new List<Npc>();
            CurrentSpawnedWaves.ForEach(wave => npcs.AddRange(wave.SpawnedNpcs));
            return npcs;
        }

        #region debug

        private void DebugPrintSpawnNpcData(Npc npc)
        {
            var s = "Spawned " + npc.Name + ", lvl " + npc.Level;
            s += ", hp " + npc.CurrentHealth;
            s += ", g " + npc.GetAttribute(AttributeName.GoldReward).Value;
            s += ", xp " + npc.GetAttribute(AttributeName.XPReward).Value;
            s += " (" + npc.Rarity + ")";

            Debug.Log(s);
        }

        public void TriggerNextSpawn()
        {
            CurrentElapsedTime = WaveCooldown;
        }

        #endregion

    }
}
