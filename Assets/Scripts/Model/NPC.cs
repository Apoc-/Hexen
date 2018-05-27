using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hexen
{
    class NPC : Entity
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int MovementSpeed { get; set; }
        public int GoldBaseReward { get; set; }
        public int XPBaseReward { get; set; }
    }
}
