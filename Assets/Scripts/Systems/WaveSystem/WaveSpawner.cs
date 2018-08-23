using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.WaveSystem
{
    class WaveSpawner : MonoBehaviour
    {
        public int WaveCooldown = 10;
        public int CurrentElapsedTime = 0;

        private int currentWave = 0;
        public int CurrentWave
        {
            get
            {
                return currentWave;
            }
        }

        private List<Wave> currentSpawnedWaves;
        private bool waitingForWave = false;


        private void OnEnable()
        {
            currentSpawnedWaves = new List<Wave>();
            CurrentElapsedTime = 0;
            currentWave = 0;
            waitingForWave = false;
        }

        private void Update()
        {
            if (currentWave >= WaveProvider.WaveCount && currentSpawnedWaves.Count == 0)
            {
                GameManager.Instance.WinGame();
            }

            if (currentSpawnedWaves.Count == 0 && !waitingForWave)
            {
                waitingForWave = true;
                StartCoroutine(WaitForNextWave());
            }

            currentSpawnedWaves.ForEach(wave =>
            {
                if (wave.SpawnCount >= wave.Size && wave.SpawnedNpcs.All(npc => npc == null))
                {
                    currentSpawnedWaves.Remove(wave);
                    GiveWaveReward(wave.WaveReward);
                }
            });
        }

        private void GiveWaveReward(WaveReward reward)
        {
            var player = GameManager.Instance.Player;
            player.IncreaseGold(reward.Gold);

            for (int i = 0; i < reward.Towers; i++)
            {
                player.AddRandomBuildableTower();
            }
        }

        private void StartSpawnWave()
        {
            if (currentWave >= WaveProvider.WaveCount) return;

            var wave = WaveProvider.ProvideWaveByID(currentWave);

            currentSpawnedWaves.Add(wave);
            waitingForWave = false;
            currentWave += 1;
            CurrentElapsedTime = 10;

            //Debug.Log("----- Spawning Wave " + currentWave + "-----");
            StartCoroutine(SpawnWave(wave, currentWave));
        }

        private IEnumerator SpawnWave(Wave wave, int lvl)
        {
            wave.SpawnCount = 0;

            while (wave.SpawnCount < wave.Size)
            {
                wave.SpawnCount += 1;
                SpawnNpc(wave, lvl);

                yield return new WaitForSeconds(wave.SpawnInterval);
            }
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

        void SpawnNpc(Wave wave, int lvl)
        {
            var go = new GameObject();
            var npc = go.AddComponent(wave.NpcType) as Npc;

            if (npc == null) return;

            npc.SetLevel(lvl);
            npc.transform.parent = transform;
            npc.name = npc.Name + "_" + wave.SpawnCount;
            npc.transform.position = GameManager.Instance.MapManager.StartTile.GetTopCenter();

            wave.SpawnedNpcs.Add(npc);

            //DebugPrintSpawnNpcData(npc);
        }


        #region debug

        private void DebugPrintSpawnNpcData(Npc npc)
        {
            var s = "Spawned " + npc.Name + ", lvl " + npc.Level;
            s += ", hp " + npc.CurrentHealth;
            s += ", g " + npc.GetAttribute(AttributeName.GoldReward).Value;
            s += ", xp " + npc.GetAttribute(AttributeName.XPReward).Value;

            Debug.Log(s);
        }

        public void DebugReset()
        {
            currentWave = 0;
            GameManager.Instance.Player.Lives = 10000;
        }

        public void TriggerNextSpawn()
        {
            CurrentElapsedTime = WaveCooldown;
        }

        #endregion

    }
}
