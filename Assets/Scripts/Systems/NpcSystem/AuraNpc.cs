using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.NpcSystem
{
    public abstract class AuraNpc : Npc
    {
        protected List<AuraEffect> AuraEffects = new List<AuraEffect>();
        protected List<IHasAttributes> AffectedAuraTargets = new List<IHasAttributes>();

        private int updateCount = 0;

        private void Update()
        {
            updateCount += 1;

            if (updateCount % 10 != 0) return;

            if (AuraEffects.Count > 0 && isSpawned)
            {
                UpdateAuraTargets();
            }

            base.Update();
        }

        public void UpdateAuraTargets()
        {
            ClearAuraTargets();

            if (!this.HasAttribute(AttributeName.AuraRange)) return;

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
            AffectedAuraTargets.Add(target);
        }

        private void ClearAuraTargets()
        {
            AffectedAuraTargets.ForEach(target =>
            {
                AuraEffects.ForEach(auraEffect =>
                {
                    var attributeEffect = auraEffect.AttributeEffect;
                    target.GetAttribute(attributeEffect.AffectedAttributeName).RemoveAttributeEffectsFromSource(this);
                });
            });

            AffectedAuraTargets = new List<IHasAttributes>();
        }

        public override void Die(bool silent = false)
        {
            ClearAuraTargets();

            base.Die(silent);
        }
    }
}