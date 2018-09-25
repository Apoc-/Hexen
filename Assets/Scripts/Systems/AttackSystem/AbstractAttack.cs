using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class AbstractAttack : MonoBehaviour
    {
        protected List<AttackEffect> AttackEffects = new List<AttackEffect>();

        public Tower Source;
        public Npc Target;

        public virtual void InitAttack(Npc target, Tower source)
        {
            InitAttackData();
            InitAttackEffects();

            this.Target = target;
            this.Source = source;
        }

        protected abstract void InitAttackData();

        protected abstract void InitAttackEffects();

        protected void ApplyEffectsToTarget(Npc target)
        {
            AttackEffects.ForEach(effect => effect.OnHit(Source, target));
        }

        protected void AddAttackEffect(AttackEffect attackEffect)
        {
            this.AttackEffects.Add(attackEffect);
        }
    }
}