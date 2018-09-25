using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
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