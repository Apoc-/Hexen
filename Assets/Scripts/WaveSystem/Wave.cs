using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen.WaveSystem;
using UnityEngine.Networking;

namespace Hexen.WaveSystem
{
    class Wave
    {
        public string NpcName;
        public int Size;
        public float SpawnInterval;
        public int SpawnCount;
        public WaveReward WaveReward;
        public List<Npc> SpawnedNpcs;

        public Wave(string npcName, int size, float spawnInterval, int goldReward, int towerReward)
        {
            NpcName = npcName;
            Size = size;
            SpawnInterval = spawnInterval;
            SpawnCount = 0;
            SpawnedNpcs = new List<Npc>();
            WaveReward = new WaveReward(goldReward, towerReward);
        }
    }
}
