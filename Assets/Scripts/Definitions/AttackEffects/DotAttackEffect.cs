using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
{
    public class DotAttackEffect : AttackEffect, AttributeEffectSource
    {
        private readonly float damagePerTick;
        private readonly float ticksPerSecond;

        private readonly float duration;

        public DotAttackEffect(float damagePerTick, float ticksPerSecond, float duration, float triggerChance = 1) : base(triggerChance)
        {
            this.damagePerTick = damagePerTick;
            this.duration = duration;
            this.ticksPerSecond = ticksPerSecond;
        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            target.ApplyDot(new NpcDot(duration, damagePerTick, ticksPerSecond, source));
        }
    }
}