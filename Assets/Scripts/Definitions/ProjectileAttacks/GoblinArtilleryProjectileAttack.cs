using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
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

            GameManager.Instance.SfxManager.AttachTrail("SmokeTrail", gameObject);
        }
    }
}