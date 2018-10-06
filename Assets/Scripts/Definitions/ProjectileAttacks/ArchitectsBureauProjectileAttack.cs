using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class ArchitectsBureauProjectileAttack : DwarfsProjectileAttack
    {
        protected override void InitAttackEffects()
        {
            base.InitAttackEffects();

            AddAttackEffect(new DamageAttackEffect());
        }
    }
}