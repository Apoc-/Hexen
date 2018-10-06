using System.Collections.Generic;
using Systems.AttackSystem;
using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class ArotasProjectileAttack : ProjectileAttack
    {
        protected override void InitAttackData()
        {
            
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();

            AddAttackEffect(new DamageAttackEffect());
        }
    }
}