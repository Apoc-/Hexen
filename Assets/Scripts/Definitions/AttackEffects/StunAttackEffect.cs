using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.SpecialEffectSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
{
    public class StunAttackEffect : AttackEffect, AttributeEffectSource
    {
        private readonly int stunDuration;

        public StunAttackEffect(int stunDuration, float triggerChance = 1) : base(triggerChance)
        {
            this.stunDuration = stunDuration;
        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            if (target.HasAttribute(AttributeName.MovementSpeed))
            {
                var movementSpeed = target.GetAttribute(AttributeName.MovementSpeed);

                var slowEffect = new AttributeEffect(
                    value: 0.0f,
                    affectedAttributeName: AttributeName.MovementSpeed,
                    effectType: AttributeEffectType.SetValue,
                    effectSource: this,
                    duration: stunDuration);

                movementSpeed.AddAttributeEffect(slowEffect);

                var effect = new ParticleEffectData("StunEffect", target.gameObject, stunDuration);
                GameManager.Instance.SpecialEffectManager.PlayParticleEffect(effect);
            }
        }
    }
}