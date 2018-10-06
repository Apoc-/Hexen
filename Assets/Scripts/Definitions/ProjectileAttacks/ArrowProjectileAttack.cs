using System.Collections.Generic;
using Systems.AttackSystem;
using Systems.GameSystem;
using Systems.SpecialEffectSystem;
using Definitions.AttackEffects;

namespace Definitions.ProjectileAttacks
{
    public class ArrowProjectileAttack : ProjectileAttack
    {
        protected override void InitAttackData()
        {
           
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();
            AddAttackEffect(new DamageAttackEffect());

            var effect = new TrailEffectData("ArrowTrail", gameObject, FlightDuration + FlightDuration/2);
            GameManager.Instance.SpecialEffectManager.PlayTrailEffect(effect);
        }
    }
}