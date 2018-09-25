using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class OrcAoeProjectileAttack : ProjectileAttack
    {
        protected override void InitAttackData()
        {
            SplashRadius = 1f;
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();

            AddAttackEffect(new DamageAttackEffect());
            AddAttackEffect(new FrenzyAttackEffect());
        }
    }
}