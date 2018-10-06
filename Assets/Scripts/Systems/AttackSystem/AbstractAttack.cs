using System.Collections.Generic;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Systems.AttackSystem
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