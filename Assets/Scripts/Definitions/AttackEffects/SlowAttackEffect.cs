using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
{
    public class SlowAttackEffect : AttackEffect, IAttributeEffectSource
    {
        private float slowAmount;

        public SlowAttackEffect(float slowAmount, float triggerChance = 1) : base(triggerChance)
        {
            this.slowAmount = slowAmount;
        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            if (target.HasAttribute(AttributeName.MovementSpeed))
            {
                var movementSpeed = target.GetAttribute(AttributeName.MovementSpeed);

                var slowEffect = new AttributeEffect(
                    value: -slowAmount,
                    affectedAttributeName: AttributeName.MovementSpeed,
                    effectType: AttributeEffectType.PercentMul,
                    effectSource: this,
                    duration: 3);

                movementSpeed.AddAttributeEffect(slowEffect);
            }
        }
    }
}