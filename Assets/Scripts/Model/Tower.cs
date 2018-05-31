using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace Hexen
{
    public class Tower : Entity
    {
        public Attribute AttackRange = new Attribute(Attribute.AttackRange, 1, 0.04f, LevelIncrementType.Percentage);
        public Attribute AttackDamage = new Attribute(Attribute.AttackDamage, 1, 0.04f, LevelIncrementType.Percentage);
        public Attribute AttackSpeed = new Attribute(Attribute.AttackSpeed, 1, 0.04f, LevelIncrementType.Percentage);

        public List<Attribute> Attributes;

        public int GoldCost;
        public string Description;

        public float WeaponHeight;
        public List<Item> Items;
        public Projectile Projectile;
        public Sprite Icon;
        public Tile Tile;

        public int Level;
        public int Xp;

        private Npc lockedTarget;
        private float lastShotFired = 0;
        
        public bool IsPlaced = false;

        [HideInInspector] public Player Owner;

        private void OnEnable()
        {
            IsPlaced = false;

            Xp = 0;
            Level = 1;

            Attributes = new List<Attribute>
            {
                AttackRange,
                AttackDamage,
                AttackSpeed
            };
        }

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

            Debug.Log("XP: " + Xp);

            while (Xp >= NextLevelXP())
            {
                Debug.Log("Level: " + Level + "->" + (Level+1));
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level += 1;
            Attributes.ForEach(attr => attr.LevelUp());
        }


        private void CheckLockedTarget()
        {
            if (lockedTarget == null)
            {
                AcquireTarget();
                return; //ea
            }

            var distance = Vector3.Distance(lockedTarget.transform.position, transform.position);

            if (distance > AttackRange.Value)
            {
                lockedTarget = null;
                AcquireTarget();
                return;
            }

            if (lastShotFired < Time.fixedTime - 1.0f / AttackSpeed.Value)
            {
                Fire();
                lastShotFired = Time.fixedTime;
            }
        }

        private void AcquireTarget()
        {
            var baseHeight = GameManager.Instance.MapManager.BaseHeight;
            var attackRange = AttackRange.Value;

            var topCap = transform.position + new Vector3(0, 5, 0);
            var botCap = new Vector3(transform.position.x, baseHeight-5, transform.position.z);

            var collidersInAttackRange = new List<Collider>(Physics.OverlapCapsule(topCap, botCap, attackRange));

            DebugExtension.DebugCapsule(topCap, botCap, attackRange);

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

        private int NextLevelXP()
        {
            var fac = 2.0f;
            var exp = 3.0f;
            
            return (int) (fac * Mathf.Pow(Level, exp));
        }

        //todo feature removed for now, incorporate as a attribute effect at a later time
        /*public float HeightDependantAttackRange()
        {
            var value = AttackRange.Value;

            if (Tile != null) //check if over tile
            {
                value *= (1 + Tile.gameObject.transform.position.y);
            }

            return value;
        }*/
    }
}
