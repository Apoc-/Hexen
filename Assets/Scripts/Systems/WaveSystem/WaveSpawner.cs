﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Systems.WaveSystem
{
    class WaveSpawner : MonoBehaviour
    {
        public int NumberOfWaves = 20;
        public int WaveCooldown = 10;
        public int CurrentElapsedTime = 0;
        private float npcSpawnInterval = 0.5f;

        private int currentWaveCount = 0;
        public int CurrentWaveCount
        {
            get
            {
                return currentWaveCount;
            }
        }

        public List<Wave> CurrentSpawnedWaves { get; private set; }
        private bool waitingForWave = false;

        //event stuff
        internal delegate void NpcHandler(Npc npc);
        public event NpcHandler OnNpcSpawned;

        private void OnEnable()
        {
            CurrentSpawnedWaves = new List<Wave>();
            CurrentElapsedTime = 0;
            currentWaveCount = 0;
            waitingForWave = false;
        }

        private void Update()
        {
            if (!GameManager.Instance.PlayerReady || GameManager.Instance.Player.GetAmbassadors() > 0) return;

            if (currentWaveCount >= NumberOfWaves && CurrentSpawnedWaves.Count == 0)
            {
                GameManager.Instance.WinGame();
            }

            if (CurrentSpawnedWaves.Count == 0 && !waitingForWave)
            {
                waitingForWave = true;
                StartCoroutine(WaitForNextWave());
            }

            CurrentSpawnedWaves.ForEach(wave =>
            {
                if (wave.NpcSpawnCount >= wave.NpcCount && wave.SpawnedNpcs.All(npc => npc == null))
                {
                    if (wave.WaveNumber % 3 == 0)
                    {
                        GameManager.Instance.Player.IncreaseAmbassadors(1);

                        Debug.Log("Granting ambassador for defeating wave #" + wave.WaveNumber);
                    }

                    //give new towers
                    GameManager.Instance.Player.AddRandomBuildableTower();
                    GameManager.Instance.Player.AddRandomBuildableTower();

                    CurrentSpawnedWaves.Remove(wave);
                }
            });
        }

        private void StartSpawnWave()
        {
            if (currentWaveCount >= NumberOfWaves) return;

            waitingForWave = false;
            currentWaveCount += 1;
            CurrentElapsedTime = WaveCooldown;

            var wave = GameManager.Instance.WaveGenerator.GenerateWave(currentWaveCount);
            wave.WaveNumber = currentWaveCount;
            CurrentSpawnedWaves.Add(wave);

            Debug.Log("----- Spawning Wave " + currentWaveCount + "-----");
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

                    yield return new WaitForSeconds(npcSpawnInterval);
                }

                wave.PackSpawnCount += 1;

                yield return new WaitForSeconds(npcSpawnInterval*2);
            }

            Debug.Log("----- Finished spawning Wave " + currentWaveCount + "-----");
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
            npc.isSpawned = true;
            npc.InitData();
            
            npc.transform.parent = transform;
            npc.name = npc.Name + " (id: " + wave.PackCount + "/" + wave.NpcSpawnCount + ")";
            npc.transform.position = GameManager.Instance.MapManager.StartTile.GetTopCenter();

            npc.InitVisuals();

            npc.gameObject.SetActive(true);
            npc.SetLevel(currentWaveCount);

            wave.SpawnedNpcs.Add(npc);
            
            DebugPrintSpawnNpcData(npc);

            if (OnNpcSpawned != null) OnNpcSpawned(npc);
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

            Debug.Log(s);
        }

        public void DebugReset()
        {
            GameManager.Instance.Player.Lives = 10000;
        }

        public void TriggerNextSpawn()
        {
            CurrentElapsedTime = WaveCooldown;
        }

        #endregion

    }
}
