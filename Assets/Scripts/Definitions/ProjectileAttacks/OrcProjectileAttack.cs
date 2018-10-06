using System.Collections.Generic;
using Systems.AttackSystem;
using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class OrcProjectileAttack : ProjectileAttack
    {
        protected override void InitAttackData()
        {
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();

            AddAttackEffect(new DamageAttackEffect());
            AddAttackEffect(new FrenzyAttackEffect());
            //AddAttackEffect(new SlowAttackEffect(0.5f));
        }
    }
}