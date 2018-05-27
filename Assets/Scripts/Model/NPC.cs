﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hexen
{
    public class Npc : Entity
    {
        public int MaxHealth;
        public int CurrentHealth;
        public float MovementSpeed;
        public int GoldReward;
        public int XPReward;
    }
}