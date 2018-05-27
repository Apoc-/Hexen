using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen
{
    public class Tower : Building
    {
        public int Level = 1;
        public int Xp = 0;
        public float AttackSpeed;
        public float AttackRange;
        public float AttackDamage;
        public List<Item> Items;
        public Projectile Projectile;

        public void Fire()
        {

        }
    }
}
