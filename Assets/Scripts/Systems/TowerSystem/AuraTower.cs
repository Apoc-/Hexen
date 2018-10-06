using System.Collections.Generic;
using System.Linq;
using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.NpcSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.TowerSystem
{
    abstract class AuraTower : Tower, IAttributeEffectSource
    {
        protected readonly List<AuraEffect> AuraEffects = new List<AuraEffect>();
        private List<IHasAttributes> _affectedAuraTargets = new List<IHasAttributes>();

        private float _lastAuraDamageTick;

        public UnityEvent OnAuraTick = new UnityEvent();

        public void Update()
        {
            if (IsPlaced)
            {
                //TODO: Refactor, runs way to often
                UpdateAuraTargets();
            }
        }

        public void UpdateAuraTargets()
        {
            ClearAuraTargets();

            if (!HasAttribute(AttributeName.AuraRange)) return;

            var pos = transform.position;
            var range = GetAttributeValue(AttributeName.AuraRange);

            foreach (var auraEffect in AuraEffects)
            {
                if (auraEffect.AffectsNpcs)
                {
                    var npcsInRange = TargetingHelper.GetNpcsInRadius(pos, range);
                    npcsInRange.ForEach(npc => ApplyAuraEffect(npc, auraEffect));
                }

                if (auraEffect.AffectsTowers)
                {
                    var towersInRange = TargetingHelper.GetTowersInRadius(pos, range);
                    towersInRange.ForEach(npc => ApplyAuraEffect(npc, auraEffect));
                }
            }
        }

        private void ApplyAuraEffect(IHasAttributes target, AuraEffect auraEffect)
        {
            var attributeEffect = auraEffect.AttributeEffect;
            var attributeName = attributeEffect.AffectedAttributeName;

            if (!target.HasAttribute(attributeName)) return;

            target.GetAttribute(attributeName).AddAttributeEffect(attributeEffect);
            _affectedAuraTargets.Add(target);
        }

        private void ClearAuraTargets()
        {
            _affectedAuraTargets.ForEach(target =>
            {
                AuraEffects.ForEach(auraEffect =>
                {
                    var attributeEffect = auraEffect.AttributeEffect;
                    target.GetAttribute(attributeEffect.AffectedAttributeName).RemoveAttributeEffectsFromSource(this);
                });
            });

            _affectedAuraTargets = new List<IHasAttributes>();
        }

        public override void Remove()
        {
            ClearAuraTargets();
            base.Remove();
        }

        protected void TickAura()
        {
            OnAuraTick.Invoke();
            
            var targets = _affectedAuraTargets.Select(it => it as Npc).Where(it => it != null).ToList();
            targets.ForEach(npc =>
            {
                var dmg = Attributes[AttributeName.AuraDamage].Value;
                npc.DealDamage(dmg, this);
            });
        }

        protected override void DoUpdate()
        {
            if (!Attributes.HasAttribute(AttributeName.AuraDamage)) return;
            if (!Attributes.HasAttribute(AttributeName.AuraTicksPerSecond)) return;

            if (_lastAuraDamageTick < Time.fixedTime - 1.0f / GetAttribute(AttributeName.AuraTicksPerSecond).Value)
            {
                TickAura();
                _lastAuraDamageTick = Time.fixedTime;
            }
        }
    }
}
