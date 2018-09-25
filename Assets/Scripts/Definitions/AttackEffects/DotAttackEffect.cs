using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.ProjectileEffects
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
            target.ApplyDot(new NpcDot(this.duration, this.damagePerTick, this.ticksPerSecond, source));
        }
    }
}