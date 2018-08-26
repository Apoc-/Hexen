using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using UnityEngine;
using UnityStandardAssets.Effects;

namespace Assets.Scripts.Systems.TowerSystem
{
    abstract class AuraTower : Tower, AttributeEffectSource
    {
        protected AuraEffect AuraEffect;
        private List<Tower> affectedTowers = new List<Tower>();

        public void Update()
        {
            if (AuraEffect != null && IsPlaced)
            {
                //TODO: Refactor, runs way to often
                UpdateAuraTargets();
            }
        }

        public void UpdateAuraTargets()
        {
            if (this.HasAttribute(AttributeName.AuraRange))
            {
                var collidersInRange = GetCollidersInAuraRange();

                foreach (var collider in collidersInRange)
                {
                    var tower = collider.GetComponentInParent<Tower>();

                    if (tower == null) continue;

                    if (affectedTowers.Contains(tower)) continue;

                    var effect = AuraEffect.AttributeEffect;
                    var attributeName = effect.AffectedAttributeName;
                    if (tower.HasAttribute(attributeName))
                    {
                        tower.GetAttribute(attributeName).AddAttributeEffect(effect);
                        affectedTowers.Add(tower);
                    }   
                }
            }
        }

        private List<Collider> GetCollidersInAuraRange()
        {
            return GetCollidersInRadius(this.GetAttribute(AttributeName.AuraRange).Value);
        }
        
        private void ClearAuraTargets()
        {
            affectedTowers.ForEach(tower =>
            {
                var effect = AuraEffect.AttributeEffect;
                tower.GetAttribute(effect.AffectedAttributeName).RemoveAttributeEffectsFromSource(this);
            });

            affectedTowers = new List<Tower>();
        }

        public override void Remove()
        {
            ClearAuraTargets();
            base.Remove();
        }

        protected override void Fire()
        {
            //does not attack
        }
    }
}
