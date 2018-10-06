using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
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