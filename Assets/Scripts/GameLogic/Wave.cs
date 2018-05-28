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
        public int size;

        public Wave(string npcName, int size)
        {
            NpcName = npcName;
            this.size = size;
        }
    }
}
