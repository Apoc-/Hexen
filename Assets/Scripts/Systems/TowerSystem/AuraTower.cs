using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Systems.TowerSystem
{
    abstract class AuraTower : Tower, AttributeEffectSource
    {
        protected List<AuraEffect> AuraEffects = new List<AuraEffect>();
        protected List<IHasAttributes> affectedAuraTargets = new List<IHasAttributes>();

        private float lastAuraDamageTick;

        public UnityEvent OnAuraTick = new UnityEvent();

        public void Update()
        {
            if (AuraEffects.Count > 0 && IsPlaced)
            {
                //TODO: Refactor, runs way to often
                UpdateAuraTargets();
            }
        }

        public void UpdateAuraTargets()
        {
            ClearAuraTargets();

            if (!this.HasAttribute(AttributeName.AuraRange)) return;

            var collidersInRange = GetCollidersInAuraRange();

            var targets = collidersInRange
                .Select(c => c.GetComponentInParent<IHasAttributes>())
                .Where(e => e != null);

            foreach (var auraTarget in targets)
            {
                if (affectedAuraTargets.Contains(auraTarget)) continue;

                foreach (var auraEffect in AuraEffects)
                {
                    if (auraTarget is Npc && !auraEffect.AffectsNpcs) continue;
                    if (auraTarget is Tower && !auraEffect.AffectsTowers) continue;

                    var attributeEffect = auraEffect.AttributeEffect;
                    var attributeName = attributeEffect.AffectedAttributeName;

                    if (!auraTarget.HasAttribute(attributeName)) continue;

                    auraTarget.GetAttribute(attributeName).AddAttributeEffect(attributeEffect);
                    affectedAuraTargets.Add(auraTarget);
                }
            }
        }

        private List<Collider> GetCollidersInAuraRange()
        {
            return GetCollidersInRadius(this.GetAttribute(AttributeName.AuraRange).Value);
        }
        
        private void ClearAuraTargets()
        {
            affectedAuraTargets.ForEach(target =>
            {
                AuraEffects.ForEach(auraEffect =>
                {
                    var attributeEffect = auraEffect.AttributeEffect;
                    target.GetAttribute(attributeEffect.AffectedAttributeName).RemoveAttributeEffectsFromSource(this);
                });
            });

            affectedAuraTargets = new List<IHasAttributes>();
        }

        public override void Remove()
        {
            ClearAuraTargets();
            base.Remove();
        }

        protected void TickAura()
        {
            this.OnAuraTick.Invoke();
            
            var targets = this.affectedAuraTargets.Select(it => it as Npc).Where(it => it != null).ToList();
            targets.ForEach(npc =>
            {
                var dmg = this.Attributes[AttributeName.AuraDamage].Value;
                npc.DealDamage(dmg, this);
            });
        }

        protected override void DoUpdate()
        {
            if (!Attributes.HasAttribute(AttributeName.AuraDamage)) return;
            if (!Attributes.HasAttribute(AttributeName.AuraTicksPerSecond)) return;

            var interval = Attributes[AttributeName.AuraTicksPerSecond].Value;

            if (lastAuraDamageTick < Time.fixedTime - 1.0f / GetAttribute(AttributeName.AuraTicksPerSecond).Value)
            {
                TickAura();
                lastAuraDamageTick = Time.fixedTime;
            }
        }
    }
}
