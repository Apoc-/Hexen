using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using JetBrains.Annotations;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Systems.TowerSystem
{
    public abstract class Tower : MonoBehaviour, IHasAttributes, AuraTarget
    {
        public AttributeContainer Attributes;
        
        public int GoldCost;
        public string Description;
        public string Name;

        protected float WeaponHeight = 0.0f;
        protected Type AttackType = null;
        protected GameObject ProjectileModelPrefab = null;

        public Sprite Icon;
        public Tile Tile;
        public GameObject ModelPrefab;
        public GameObject ModelGameObject;

        public int Level = 1;
        public int Xp = 0;

        protected Npc LockedTarget;
        private float lastShotFired;
        
        public bool IsPlaced = false;

        public Rarities Rarity;
        public FactionNames Faction = FactionNames.Humans;

        public float Height { get; private set; }

        //events
        public delegate void OnAttackEvent(Npc attackTarget);
        public event OnAttackEvent OnAttack;

        public delegate void LevelUpEvent(int level);
        public event LevelUpEvent OnLevelUp;

        [HideInInspector] public Player Owner;

        public void InitTower()
        {
            InitTowerData();
            InitAttributes();
            InitTowerModel();
            InitTowerEffects();

            InvokeRepeating("RemoveFinishedTimedAttributeEffects", 0, 1);
        }
        
        public abstract void InitTowerData();

        public void InitTowerModel()
        {
            if (ModelGameObject == null)
            {
                ModelGameObject = Instantiate(ModelPrefab);
                ModelGameObject.transform.SetParent(transform, false);
                ModelGameObject.layer = LayerMask.NameToLayer("Towers");
            }

            Height = GetComponentInChildren<MeshFilter>().mesh.bounds.max.y;
        }

        public void InitTowerEffects()
        {

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
            var c = GameSettings.LvlConst;
            var fac = GameSettings.LvlFact;
            var exp = GameSettings.LvlExp;
            
            return (int)(c + fac * Mathf.Pow(Level, exp));
        }

        private void LevelUp()
        {
            Level += 1;

            OnLevelUp?.Invoke(Level);

            foreach (var keyValuePair in Attributes)
            {
                keyValuePair.Value.LevelUp();
            }

            PlayLevelUpEffect();
        }

        protected virtual void DoUpdate()
        {
            CheckTarget();

            if (lastShotFired < Time.fixedTime - 1.0f / GetAttribute(AttributeName.AttackSpeed).Value)
            {
                Attack();
                lastShotFired = Time.fixedTime;
            }
        }

        private void CheckTarget()
        {
            if (!this.HasAttribute(AttributeName.AttackRange)) return;

            var targetingHelper = GameManager.Instance.TargetingHelper;
            if (targetingHelper.CurrentTargetInRange(this))
            {
                LockedTarget = targetingHelper.CurrentTargetedNpc;
            }
            else
            {
                var range = GetAttributeValue(AttributeName.AttackRange);

                if (LockedTarget != null)
                {    
                    var dist = Vector3.Distance(LockedTarget.gameObject.transform.position, transform.position);
                    if (dist > range)
                    {
                        LockedTarget = AquireTargetInRange(range);
                    }
                }
                else
                {
                    LockedTarget = AquireTargetInRange(range);
                }
            }
        }

        private Npc AquireTargetInRange(float range)
        {
            var npcs = TargetingHelper.GetNpcsInRadius(transform.position, range);
            return npcs.FirstOrDefault();
        }

        protected virtual void Attack(bool triggering = true)
        {
            if (LockedTarget == null) return;

            if (triggering)
            {
                OnAttack?.Invoke(LockedTarget);
            }
            
            InitializeAttack(); 
        }

        private void InitializeAttack()
        {
            GameObject go;
            if (ProjectileModelPrefab != null)
            {
                go = Instantiate(ProjectileModelPrefab);
            }
            else
            {
                go = new GameObject(AttackType.Name);
            }
            
            var attack = (AbstractAttack) go.AddComponent(AttackType);

            attack.transform.SetParent(this.transform);
            attack.transform.localPosition = new Vector3(0, WeaponHeight, 0);

            attack.InitAttack(LockedTarget, this);
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

        public float GetAttributeValue(AttributeName attrName)
        {
            return Attributes[attrName].Value;
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

        public virtual void Remove()
        {
            Destroy(gameObject);
        }

        public void Stun(float duration, AttributeEffectSource source)
        {
            if (this.HasAttribute(AttributeName.AttackSpeed))
            {
                var attr = GetAttribute(AttributeName.AttackSpeed);
                if(attr.Value > 0)
                {
                    var effect = new AttributeEffect(0.0f, AttributeName.AttackSpeed, AttributeEffectType.SetValue, source, duration);
                    attr.AddAttributeEffect(effect);
                }
            }

            PlayParticleEffectAboveTower("StunEffect", duration);
        }

        public void PlayParticleEffectAboveTower(string effectPrefabName, float duration)
        {
            var offset = new Vector3(0, Height, 0);
            var pe = new ParticleEffectData(effectPrefabName, gameObject, offset, duration);
            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(pe);
        }

        public void PlayLevelUpEffect()
        {
            var pe = new ParticleEffectData("LevelUpEffect", gameObject, 3f);
            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(pe);
        }

        public void ForceRetarget()
        {
            this.LockedTarget = null;
        }
    }
}
