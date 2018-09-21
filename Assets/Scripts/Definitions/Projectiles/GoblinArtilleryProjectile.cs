using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class GoblinArtilleryProjectile : DirectAoeProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 15;
            Radius = 2f;
            Velocity = new Vector3(0,10.0f,0);
            VelocityDampeningFactor = 0.25f;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());

            GameManager.Instance.SfxManager.AttachTrail("SmokeTrail", gameObject);
        }
    }
}