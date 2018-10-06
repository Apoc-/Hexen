using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
{
    public class DotAttackEffect : AttackEffect, IAttributeEffectSource
    {
        private readonly float _damagePerTick;
        private readonly float _ticksPerSecond;

        private readonly float _duration;

        public DotAttackEffect(float damagePerTick, float ticksPerSecond, float duration, float triggerChance = 1) : base(triggerChance)
        {
            _damagePerTick = damagePerTick;
            _duration = duration;
            _ticksPerSecond = ticksPerSecond;
        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            target.ApplyDot(new NpcDot(_duration, _damagePerTick, _ticksPerSecond, source));
        }
    }
}