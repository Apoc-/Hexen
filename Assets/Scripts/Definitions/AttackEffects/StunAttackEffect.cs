using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.SpecialEffectSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
{
    public class StunAttackEffect : AttackEffect, IAttributeEffectSource
    {
        private readonly int _stunDuration;

        public StunAttackEffect(int stunDuration, float triggerChance = 1) : base(triggerChance)
        {
            _stunDuration = stunDuration;
        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            if (target.HasAttribute(AttributeName.MovementSpeed))
            {
                var movementSpeed = target.GetAttribute(AttributeName.MovementSpeed);

                var slowEffect = new AttributeEffect(
                    0.0f,
                    AttributeName.MovementSpeed,
                    AttributeEffectType.SetValue,
                    this,
                    _stunDuration);

                movementSpeed.AddAttributeEffect(slowEffect);

                var effect = new ParticleEffectData("StunEffect", target.gameObject, _stunDuration);
                GameManager.Instance.SpecialEffectManager.PlayParticleEffect(effect);
            }
        }
    }
}