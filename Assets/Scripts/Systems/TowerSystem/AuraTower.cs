using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;

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
            var collidersInAttackRange = GetCollidersInAttackRange();

            foreach (var collider in collidersInAttackRange)
            {
                var tower = collider.GetComponentInParent<Tower>();
                
                if (tower == null) continue;

                if (affectedTowers.Contains(tower)) continue;

                var effect = AuraEffect.AttributeEffect;
                tower.GetAttribute(effect.AffectedAttributeName).AddAttributeEffect(effect);
                affectedTowers.Add(tower);
            }
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
    }
}
