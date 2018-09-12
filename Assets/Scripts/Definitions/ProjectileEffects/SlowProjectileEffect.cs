using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class SlowProjectileEffect : ProjectileEffect, AttributeEffectSource
    {
        private float slowAmount;

        public SlowProjectileEffect(float slowAmount, float triggerChance = 1) : base(triggerChance)
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