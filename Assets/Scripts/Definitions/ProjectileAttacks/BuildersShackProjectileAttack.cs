using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class BuildersShackProjectileAttack : DwarfsProjectileAttack
    {
        protected override void InitAttackEffects()
        {
            base.InitAttackEffects();

            AddAttackEffect(new DamageAttackEffect());
        }
    }
}