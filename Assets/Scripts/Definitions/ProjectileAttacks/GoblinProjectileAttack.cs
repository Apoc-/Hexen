using System.Collections.Generic;
using Systems.AttackSystem;
using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class GoblinProjectileAttack : ProjectileAttack
    {
        protected override void InitAttackData()
        {
            SplashRadius = 0.75f;
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();

            AddAttackEffect(new DamageAttackEffect());
        }
    }
}