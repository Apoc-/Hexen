using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Hexen;
using Hexen.WaveSystem;
using UnityEngine;

namespace Hexen.WaveSystem
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
            
            StartCoroutine(SpawnWave(wave));
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            wave.SpawnCount = 0;

            while (wave.SpawnCount < wave.Size)
            {
                wave.SpawnCount += 1;
                SpawnNpc(wave);

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

        void SpawnNpc(Wave wave)
        {
            var npc = Instantiate(Resources.Load<Npc>("Prefabs/Entities/Npcs/" + wave.NpcName));

            npc.transform.parent = transform.parent;
            npc.name = npc.Name + "_" + wave.SpawnCount;
            npc.transform.position = GameManager.Instance.MapManager.StartTile.GetTopCenter();

            wave.SpawnedNpcs.Add(npc);
        }


        #region debug

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
