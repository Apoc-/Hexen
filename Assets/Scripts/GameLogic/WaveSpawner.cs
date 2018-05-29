using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    class WaveSpawner : MonoBehaviour
    {
        public int WaveCooldown = 30;
        public int CurrentWaveCooldown = 0;

        private int currentWave = 0;
        public int CurrentWave
        {
            get
            {
                return currentWave;
            }
        }

        private int totalWaves = 0;
        public int TotalWaves
        {
            get
            {
                return totalWaves;
            }
        }

        private Queue<Wave> waves;
        private int waveNpcSpawnCount = 0;

        private Coroutine waitForNextWaveCoroutine;

        private void Start()
        {
            waves = LoadWaveData();
            totalWaves = waves.Count;
        }

        private Queue<Wave> LoadWaveData()
        {
            var waves = new Queue<Wave>();
            
            waves.Enqueue(new Wave("Rat", 1, 0.25f));
            waves.Enqueue(new Wave("Rat", 2, 0.25f));
            waves.Enqueue(new Wave("Rat", 4, 0.25f));
            waves.Enqueue(new Wave("Rat", 8, 0.25f));

            return waves;
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            while (waveNpcSpawnCount < wave.Size)
            {
                waveNpcSpawnCount += 1;
                SpawnNpc(wave.NpcName);

                yield return new WaitForSeconds(wave.SpawnInterval);
            }

            waveNpcSpawnCount = 0;

            HandleWaveSpawnFinished();
        }

        private void HandleWaveSpawnFinished()
        {
            var gm = GameManager.Instance;
            gm.TowerBuildManager.AddRandomBuildableTower(gm.Player);

            waitForNextWaveCoroutine = StartCoroutine(WaitForNextWave());
        }

        IEnumerator WaitForNextWave()
        {
            while (CurrentWaveCooldown < WaveCooldown)
            {
                CurrentWaveCooldown += 1;
                
                yield return new WaitForSeconds(1.0f);
            }

            NextWave();
        }

        void SpawnNpc(string waveNpcName)
        {
            var npc = Instantiate(Resources.Load<Npc>("Prefabs/Entities/Npcs/" + waveNpcName));

            npc.transform.parent = transform.parent;
            npc.name = npc.Name + "_" + waveNpcSpawnCount;
            npc.transform.position = GameManager.Instance.MapManager.StartTile.GetTopCenter();
        }

        public void NextWave()
        {
            CurrentWaveCooldown = 0;
            if (waitForNextWaveCoroutine != null)
            {
                StopCoroutine(waitForNextWaveCoroutine);
            }
            
            if (waves.Count > 0)
            {
                currentWave += 1;
                StartCoroutine(SpawnWave(waves.Dequeue()));
            }
            else
            {
                if (FindObjectsOfType<Npc>().Length <= 0)
                {
                    GameManager.Instance.WinGame();
                }
            };
        }
    }
}
