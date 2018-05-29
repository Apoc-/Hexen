using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
{
    public class Tower : AttributeEntity
    {
        public int GoldCost;
        public int Level = 1;
        public int Xp = 0;
        public float AttackSpeed;
        public float BaseAttackRange;
        public int AttackDamage;
        public float WeaponHeight;
        public List<Item> Items;
        public Projectile Projectile;
        public Sprite Icon;
        public Tile Tile;

        private Npc lockedTarget;
        private float lastShotFired = 0;
        
        public bool IsPlaced = false;

        [HideInInspector] public Player Owner;
        
        private void FixedUpdate()
        {
            if (IsPlaced)
            {
                CheckLockedTarget();
            }
        }

        public void GiveXP(int amount)
        {
            Xp += amount;
        }


        private void CheckLockedTarget()
        {
            if (lockedTarget == null)
            {
                AcquireTarget();
                return; //ea
            }

            var distance = Vector3.Distance(lockedTarget.transform.position, transform.position);

            if (distance > BaseAttackRange)
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
            var baseHeight = GameManager.Instance.MapManager.BaseHeight;
            var actualAttackRange = ActualAttackRange();

            var topCap = transform.position + new Vector3(0, 5, 0);
            var botCap = new Vector3(transform.position.x, baseHeight-5, transform.position.z);

            var collidersInAttackRange = new List<Collider>(Physics.OverlapCapsule(topCap, botCap, actualAttackRange));

            DebugExtension.DebugCapsule(topCap, botCap, actualAttackRange);

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
            shot.Source = this;
        }

        public float ActualAttackRange()
        {
            return BaseAttackRange * (1 + Tile.gameObject.transform.position.y);
        }
    }
}
