using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class LaurinsVaultProjectileAttack : DwarfsProjectileAttack
    {
        protected override void InitAttackEffects()
        {
            base.InitAttackEffects();

            AddAttackEffect(new LaurinsVaultAttackEffect());
            AddAttackEffect(new EnableRandomTowerAttackEffect(0.05f));
        }
    }
}