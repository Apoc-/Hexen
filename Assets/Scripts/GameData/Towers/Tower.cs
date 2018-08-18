using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ProjectileSystem;
using Hexen.AttributeSystem;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
{
    public abstract class Tower : MonoBehaviour, IHasAttributes
    {
        // TODO: Protected. Apoc- told me I should not change the UI so I don't
        // TODO: Yes Apoc- did, because Mijago likes to change things everywhere, even if not needed right now :D
        public AttributeContainer Attributes;
        
        public int GoldCost;
        public string Description;
        public string Name;

        public float WeaponHeight;
        public Type ProjectileType;
        public GameObject ProjectileModel;

        public Sprite Icon;
        public Tile Tile;
        public GameObject Model;

        public int Level = 1;
        public int Xp = 0;

        private Npc lockedTarget;
        private float lastShotFired;
        
        public bool IsPlaced = false;

        public List<Item> Items;

        [HideInInspector] public Player Owner;

        private void Awake()
        {
            InitTower();
            InitAttributes();
            InitTowerModel();
        }
        
        public abstract void InitTower();

        public void InitTowerModel()
        {
            var mdlGo = Instantiate(Model);
            mdlGo.transform.SetParent(transform, false);
        }

        public void InitCopyTowerData(Tower source)
        {
            /*Attributes = new List<Attribute>();

            source.Attributes.ForEach(attr =>
            {
                Attributes.Add(new Attribute(attr));
            });*/

            this.GiveXP(source.Xp);
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

            while (Xp >= NextLevelXP())
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level += 1;

            foreach (var keyValuePair in Attributes)
            {
                keyValuePair.Value.LevelUp();
            }
        }

        private void CheckLockedTarget()
        {
            if (lockedTarget == null)
            {
                AcquireTarget();
                return; //ea
            }

            var distance = Vector3.Distance(lockedTarget.transform.position, transform.position);

            if (distance > GetAttribute(AttributeName.AttackRange).Value)
            {
                lockedTarget = null;
                AcquireTarget();
                return;
            }

            if (lastShotFired < Time.fixedTime - 1.0f / GetAttribute(AttributeName.AttackSpeed).Value)
            {
                Fire();
                lastShotFired = Time.fixedTime;
            }
        }

        private void AcquireTarget()
        {
            var collidersInAttackRange = GetCollidersInAttackRange();

            foreach (var collider in collidersInAttackRange)
            {
                if (collider.transform.parent.GetComponent<Npc>() == null) continue;
                lockedTarget = collider.transform.parent.GetComponent<Npc>();

            }
        }

        protected List<Collider> GetCollidersInAttackRange()
        {
            var baseHeight = GameManager.Instance.MapManager.BaseHeight;
            var attackRange = GetAttribute(AttributeName.AttackRange).Value;

            var topCap = transform.position + new Vector3(0, 5, 0);
            var botCap = new Vector3(transform.position.x, baseHeight - 5, transform.position.z);

            return new List<Collider>(Physics.OverlapCapsule(topCap, botCap, attackRange));
        }

        private void Fire()
        {
            SpawnProjectile();
        }

        private void SpawnProjectile()
        {
            var go = Instantiate(ProjectileModel);

            var projectile = (Projectile) go.AddComponent(ProjectileType);

            projectile.transform.SetParent(this.transform);
            projectile.transform.localPosition = new Vector3(0, WeaponHeight, 0);

            projectile.Target = this.lockedTarget;
            projectile.Source = this;
        }

        private int NextLevelXP()
        {
            var fac = 2.0f;
            var exp = 3.0f;
            
            return (int) (fac * Mathf.Pow(Level, exp));
        }
        protected virtual void InitAttributes()
        {
            Attributes = new AttributeContainer();
            AddAttribute(new Attribute(AttributeName.CritChance, 0.1f, 0, LevelIncrementType.Flat));
        }

        public void AddAttribute(Attribute attr)
        {
            Attributes.AddAttribute(attr);
        }

        public Attribute GetAttribute(AttributeName attrName)
        {
            return Attributes[attrName];
        }

        public bool HasAttribute(AttributeName attrName)
        {
            return Attributes.HasAttribute(attrName);
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

        public virtual void Remove()
        {
            Destroy(gameObject);
        }

        protected virtual void OnCrit()
        {

        }
    }
}
