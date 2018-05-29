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

        private void Update()
        {
            
        }

        private void Start()
        {
            waves = LoadWaveData();
            totalWaves = waves.Count;
        }

        private Queue<Wave> LoadWaveData()
        {
            var waves = new Queue<Wave>();
            
            waves.Enqueue(new Wave("Rat", 10));
            waves.Enqueue(new Wave("Rat", 5));

            return waves;
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            while (waveNpcSpawnCount < wave.size)
            {
                waveNpcSpawnCount += 1;
                SpawnNpc(wave.NpcName);

                yield return new WaitForSeconds(1.5f);
            }

            waveNpcSpawnCount = 0;
        }

        void SpawnNpc(string waveNpcName)
        {
            var npc = Instantiate(Resources.Load<Npc>("Prefabs/Entities/Npcs/" + waveNpcName));

            npc.transform.parent = transform.parent;
            npc.name = npc.Name + "_" + waveNpcSpawnCount;
            npc.transform.position = MapManager.Instance.StartTile.transform.position;
        }

        public void NextWave()
        {
            if (waves.Count > 0)
            {
                currentWave += 1;
                StartCoroutine(SpawnWave(waves.Dequeue()));
            };
        }
    }
}
