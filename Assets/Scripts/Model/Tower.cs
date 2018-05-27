using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private Npc lockedTarget;

        private void CheckLockedTarget()
        {
            if (lockedTarget == null)
            {
                AcquireTarget();
                return; //ea
            }

            var distance = Vector3.Distance(lockedTarget.transform.position, transform.position);

            if (distance > AttackRange)
            {
                lockedTarget = null;
                AcquireTarget();
            }
        }

        private void AcquireTarget()
        {
            var collidersInAttackRange = new List<Collider>(Physics.OverlapSphere(transform.position, AttackRange));

            lockedTarget = collidersInAttackRange
                .First(c => c.GetComponent<Npc>() != null)
                .GetComponent<Npc>();
        }

        private void Fire()
        {

        }
    }
}
