using System;
using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;
using UnityEngine.Events;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Systems.TowerSystem
{
    public abstract class Tower : MonoBehaviour, IHasAttributes, AuraTarget
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

        protected Npc lockedTarget;
        private float lastShotFired;
        
        public bool IsPlaced = false;

        public Rarities Rarity;
        public FactionNames Faction = FactionNames.Humans;

        public UnityEvent OnAttack = new UnityEvent();
        public UnityEvent OnLevelUp = new UnityEvent();

        [HideInInspector] public Player Owner;



        private void Awake()
        {
            InitTower();
            InitAttributes();
            InitTowerModel();
            InitTowerEffects();
            InvokeRepeating("RemoveFinishedTimedAttributeEffects", 0, 1);
        }
        
        public abstract void InitTower();

        public void InitTowerModel()
        {
            var mdlGo = Instantiate(Model);
            mdlGo.transform.SetParent(transform, false);
        }

        public void InitTowerEffects()
        {
            this.OnLevelUp.AddListener(() =>
            {
                GameManager.Instance.SfxManager.PlaySpecialEffect(this, "LevelUpEffect");
            });
        }

        public void InitCopyTowerData(Tower source)
        {
            this.GiveXP(source.Xp);
        }

        private void FixedUpdate()
        {
            if (IsPlaced)
            {
                DoUpdate();
            }
        }

        public void GiveXP(int amount)
        {
            Xp += amount;

            while (Xp >= NextLevelXP() && this.Level < GameSettings.TowerMaxLevel)
            {
                LevelUp();
            }
        }
        private int NextLevelXP()
        {
            var fac = 2f;
            var exp = 2.5f;
            var c = 10f;

            return (int)(c + fac * Mathf.Pow(Level, exp));
        }

        private void LevelUp()
        {
            OnLevelUp.Invoke();
            Level += 1;

            foreach (var keyValuePair in Attributes)
            {
                keyValuePair.Value.LevelUp();
            }
        }

        protected virtual void DoUpdate()
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
            if (this.HasAttribute(AttributeName.AttackRange))
            {
                var collidersInAttackRange = GetCollidersInRadius(GetAttribute(AttributeName.AttackRange).Value);

                foreach (var collider in collidersInAttackRange)
                {
                    if (collider.transform.parent.GetComponent<Npc>() == null) continue;
                    lockedTarget = collider.transform.parent.GetComponent<Npc>();
                }
            }
        }

        //todo refactor, npc has the same 
        public List<Collider> GetCollidersInRadius(float radius)
        {
            var baseHeight = GameManager.Instance.MapManager.BaseHeight;

            var topCap = transform.position + new Vector3(0, 5, 0);
            var botCap = new Vector3(transform.position.x, baseHeight - 5, transform.position.z);

            return new List<Collider>(Physics.OverlapCapsule(topCap, botCap, radius));
        }

        protected virtual void Fire()
        {
            OnAttack.Invoke();
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

        
        protected virtual void InitAttributes()
        {
            Attributes = new AttributeContainer();
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

        public void RemoveFinishedTimedAttributeEffects()
        {
            foreach (var pair in this.Attributes)
            {
                pair.Value.RemovedFinishedAttributeEffects();
            }
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
