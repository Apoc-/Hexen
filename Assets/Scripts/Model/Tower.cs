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
        public float WeaponHeight;
        public List<Item> Items;
        public Projectile Projectile;

        private Npc lockedTarget;
        private float lastShotFired = 0;

        private void FixedUpdate()
        {
            CheckLockedTarget();
        }

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
                return;
            }

            if (lastShotFired < Time.fixedTime - 1 / this.AttackSpeed)
            {
                Fire();
                lastShotFired = Time.fixedTime;
            }
        }

        private void AcquireTarget()
        {
            var collidersInAttackRange = new List<Collider>(Physics.OverlapSphere(transform.position, AttackRange));

            foreach (var collider in collidersInAttackRange)
            {
                if (collider.transform.parent.GetComponent<Npc>() == null) continue;
                lockedTarget = collider.transform.parent.GetComponent<Npc>();

            }
        }

        private void Fire()
        {
            var shot = Instantiate(this.Projectile);
            shot.transform.SetParent(this.transform);
            shot.transform.localPosition = new Vector3(0, WeaponHeight, 0);
            shot.Target = this.lockedTarget;
        }
    }
}
