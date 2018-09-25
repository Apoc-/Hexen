using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class FrenzyAttackEffect : AttackEffect, AttributeEffectSource
    {
        protected override void ApplyEffect(Tower source, Npc target)
        {
            if (source.HasAttribute(AttributeName.AttackSpeed))
            {
                var attr = source.GetAttribute(AttributeName.AttackSpeed);
                var effect = new AttributeEffect(0.1f, AttributeName.AttackSpeed, AttributeEffectType.PercentAdd, this, 3);

                attr.AddAttributeEffect(effect);
            }
        }
    }
}