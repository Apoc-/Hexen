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
        private Queue<Wave> waves;
        private int waveNpcSpawnCount = 0;

        private void Update()
        {
            
        }

        private void Start()
        {
            waves = LoadWaveData();
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
            currentWave += 1;
            if (waves.Count > 0)
            {
                StartCoroutine(SpawnWave(waves.Dequeue()));
            };
        }
    }
}
