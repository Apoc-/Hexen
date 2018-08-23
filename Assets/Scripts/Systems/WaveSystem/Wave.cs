using System;
using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;

namespace Assets.Scripts.Systems.WaveSystem
{
    class Wave
    {
        public Type NpcType;
        public int Size;
        public float SpawnInterval;
        public int SpawnCount;
        public WaveReward WaveReward;
        public List<Npc> SpawnedNpcs;

        public Wave(Type npcType, int size, float spawnInterval, int goldReward, int towerReward, int ambassadorReward = 0)
        {
            NpcType = npcType;
            Size = size;
            SpawnInterval = spawnInterval;
            SpawnCount = 0;
            SpawnedNpcs = new List<Npc>();
            WaveReward = new WaveReward(goldReward, towerReward, ambassadorReward);
        }
    }
}
