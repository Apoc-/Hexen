using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace Assets.Scripts.GameLogic
{
    class Wave
    {
        public string NpcName;
        public int Size;
        public float SpawnInterval;

        public Wave(string npcName, int size, float spawnInterval)
        {
            NpcName = npcName;
            Size = size;
            SpawnInterval = spawnInterval;
        }
    }
}
