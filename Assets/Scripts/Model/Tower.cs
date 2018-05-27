using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen
{
    public class Tower : Building
    {
        public int Level { get; set; }
        public int XP { get; set; }
        public float AttackSpeed { get; set; }
        public float AttackRange { get; set; }
        public float AttackDamage { get; set; }
        public List<Item> Items { get; set; }
        public Weapon Weapon { get; set; }
    }
}
