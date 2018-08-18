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

            if (target.HasAttribute(AttributeName.Health))
            {
                target.DealDamage(dmg);
                var hp = target.GetAttribute(AttributeName.Health).Value;

                if (hp <= 0f)
                {
                    target.Kill();
                    GiveRewards(source, target);
                }
            }
        }

        private void GiveRewards(Tower source, Npc target)
        {
            if (target.HasAttribute(AttributeName.XPReward))
            {
                GiveXP(source, target.GetAttribute(AttributeName.XPReward).Value);
            }

            if (target.HasAttribute(AttributeName.GoldReward))
            {
                GiveGold(source.Owner, target.GetAttribute(AttributeName.GoldReward).Value);
            }
        }

        private void GiveXP(Tower target, float amount)
        {
            target.GiveXP((int) amount);
        }

        private void GiveGold(Player target, float amount)
        {
            target.IncreaseGold((int) amount);
        }
    }
}