﻿using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class StunProjectileEffect : ProjectileEffect, AttributeEffectSource
    {
        private readonly int stunDuration;

        public StunProjectileEffect(int stunDuration, float triggerChance = 1) : base(triggerChance)
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
            }
        }
    }
}