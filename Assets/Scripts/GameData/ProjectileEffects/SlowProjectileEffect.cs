using System.Runtime.CompilerServices;
using Assets.Scripts.AttributeSystem;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
{
    public class SlowProjectileEffect : ProjectileEffect, AttributeEffectSource
    {
        protected override void ApplyEffect(Tower source, Npc target)
        {
            if (target.HasAttribute(AttributeName.MovementSpeed))
            {
                var movementSpeed = target.GetAttribute(AttributeName.MovementSpeed);

                var slowEffect = new TimedAttributeEffect(
                    value: -0.1f,
                    affectedAttribute: movementSpeed,
                    effectType: AttributeEffectType.PercentMul,
                    effectSource: this,
                    duration: 3000.0f);

                movementSpeed.AddAttributeEffect(slowEffect);

                Debug.Log("Applied slow " + slowEffect.GetHashCode());
            }
        }
    }
}