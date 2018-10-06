using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
{
    public class LaurinsVaultAttackEffect : AttackEffect
    {
        protected override void ApplyEffect(Tower source, Npc target)
        {
            var dmg = 0f;
            if (source.HasAttribute(AttributeName.AttackDamage))
            {
                dmg = source.Attributes.GetAttribute(AttributeName.AttackDamage).Value;
            }

            dmg += source.Owner.Gold * 0.1f;

            target.HitNpc(new NpcHitData(dmg, source));
        }
    }
}