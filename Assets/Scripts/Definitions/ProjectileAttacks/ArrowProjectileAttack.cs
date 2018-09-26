using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
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