using System;
using System.Collections.Generic;
using System.Linq;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
{
    public class Tower : MonoBehaviour
    {
        private Dictionary<AttributeName, Attribute> attributeDictionary = new Dictionary<AttributeName, Attribute>();
        public List<Attribute> Attributes = new List<Attribute>();
        
        public int GoldCost;
        public string Description;
        public string Name;

        public float WeaponHeight;
        public Projectile Projectile;
        public Sprite Icon;
        public Tile Tile;
        public GameObject Model;

        public int Level;
        public int Xp;

        private Npc lockedTarget;
        private float lastShotFired;
        
        public bool IsPlaced = false;

        public List<Item> Items;

        [HideInInspector] public Player Owner;

        public virtual void InitTowerData()
        {
            attributeDictionary = new Dictionary<AttributeName, Attribute>();
            Attributes = new List<Attribute>();

            IsPlaced = false;

            Xp = 0;
            Level = 1;
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

        public void AddAttribute(Attribute attr)
        {
            Attributes.Add(attr);
            attributeDictionary.Add(attr.AttributeName, attr);
        }

        public Attribute GetAttribute(AttributeName attrName)
        {
            if (Attributes.Count != attributeDictionary.Count)
            {
                BuildAttributeDictionary();
            }

            return attributeDictionary[attrName];
        }

        private void BuildAttributeDictionary()
        {
            attributeDictionary = new Dictionary<AttributeName, Attribute>();
            Attributes.ForEach(attr => attributeDictionary.Add(attr.AttributeName, attr));
        }

        public void InitTowerModel()
        {
            var mdlGo = Instantiate(Model);
            mdlGo.transform.SetParent(transform, false);
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

    }
}
