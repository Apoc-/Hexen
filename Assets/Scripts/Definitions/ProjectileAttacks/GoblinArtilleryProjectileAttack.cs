using System.Collections.Generic;
using Systems.AttackSystem;
using Systems.GameSystem;
using Systems.SpecialEffectSystem;
using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class GoblinArtilleryProjectileAttack : ProjectileAttack
    {
        protected override void InitAttackData()
        {
            SplashRadius = 2f;
            FlightDuration = 1;
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();

            AddAttackEffect(new DamageAttackEffect());

            var trail = new TrailEffectData("SmokeTrail", gameObject, FlightDuration + 0.5f);
            GameManager.Instance.SpecialEffectManager.PlayTrailEffect(trail);
        }
    }
}