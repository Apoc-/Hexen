using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
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

            var collidersInRange = GetCollidersInAuraRange(GameSettings.NpcLayerMask);
            collidersInRange.AddRange(GetCollidersInAuraRange(GameSettings.TowersLayerMask));

            var targets = collidersInRange
                .Select(c => c.GetComponentInParent<IHasAttributes>())
                .Where(e => e != null);

            foreach (var auraTarget in targets)
            {
                if (AffectedAuraTargets.Contains(auraTarget)) continue;

                foreach (var auraEffect in AuraEffects)
                {
                    if (auraTarget is Npc && !auraEffect.AffectsNpcs) continue;
                    if (auraTarget is Tower && !auraEffect.AffectsTowers) continue;

                    var attributeEffect = auraEffect.AttributeEffect;
                    var attributeName = attributeEffect.AffectedAttributeName;

                    if (!auraTarget.HasAttribute(attributeName)) continue;

                    auraTarget.GetAttribute(attributeName).AddAttributeEffect(attributeEffect);
                    AffectedAuraTargets.Add(auraTarget);
                }
            }
        }

        private List<Collider> GetCollidersInAuraRange(int layerMask)
        {
            return GetCollidersInRadius(this.GetAttribute(AttributeName.AuraRange).Value, layerMask);
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