using System.Collections.Generic;
using Systems.AttackSystem;
using Systems.AttributeSystem;

namespace Systems.NpcSystem
{
    public abstract class AuraNpc : Npc
    {
        protected readonly List<AuraEffect> AuraEffects = new List<AuraEffect>();
        private List<IHasAttributes> _affectedAuraTargets = new List<IHasAttributes>();
        private int _updateCount;

        private new void Update()
        {
            _updateCount += 1;

            if (_updateCount % 10 != 0) return;

            if (AuraEffects.Count > 0 && IsSpawned)
            {
                UpdateAuraTargets();
            }

            base.Update();
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

        public override void Die(bool silent = false)
        {
            ClearAuraTargets();

            base.Die(silent);
        }
    }
}