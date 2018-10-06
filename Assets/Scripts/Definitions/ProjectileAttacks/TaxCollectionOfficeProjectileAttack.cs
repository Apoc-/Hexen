using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class TaxCollectionOfficeProjectileAttack : DwarfsProjectileAttack
    {
        protected override void InitAttackEffects()
        {
            base.InitAttackEffects();

            AddAttackEffect(new DamageAttackEffect());
        }
    }
}