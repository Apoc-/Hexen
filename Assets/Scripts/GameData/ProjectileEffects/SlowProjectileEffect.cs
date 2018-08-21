using System.Runtime.CompilerServices;
using Assets.Scripts.AttributeSystem;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
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

                var slowEffect = new TimedAttributeEffect(
                    value: -slowAmount,
                    affectedAttribute: movementSpeed,
                    effectType: AttributeEffectType.PercentMul,
                    effectSource: this,
                    duration: 3000.0f);

                movementSpeed.AddAttributeEffect(slowEffect);
            }
        }
    }
}