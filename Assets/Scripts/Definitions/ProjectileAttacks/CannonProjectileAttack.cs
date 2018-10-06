using System.Collections.Generic;
using Systems.AttackSystem;
using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class CannonProjectileAttack : ProjectileAttack
    {
        protected override void InitAttackData()
        {
            SplashRadius = 1.5f;
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();
            AddAttackEffect(new DamageAttackEffect());
        }
    }
}