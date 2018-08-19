using System;
using Hexen.GameData.Towers;

namespace Hexen
{
    public class DamageProjectileEffect : ProjectileEffect
    {
        protected override void ApplyEffect(Tower source, Npc target)
        {
            var dmg = 0f;
            if (source.HasAttribute(AttributeName.AttackDamage))
            {
                dmg = source.Attributes.GetAttribute(AttributeName.AttackDamage).Value;
            }

            target.DealDamage(dmg, source);
        }
    }
}