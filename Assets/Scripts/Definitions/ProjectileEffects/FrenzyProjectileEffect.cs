using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class FrenzyProjectileEffect : ProjectileEffect, AttributeEffectSource
    {
        protected override void ApplyEffect(Tower source, Npc target)
        {
            if (source.HasAttribute(AttributeName.AttackSpeed))
            {
                var attr = source.GetAttribute(AttributeName.AttackSpeed);
                var effect = new TimedAttributeEffect(0.25f, attr, AttributeEffectType.PercentAdd, this, 3000f);

                attr.AddAttributeEffect(effect);
            }
        }
    }
}