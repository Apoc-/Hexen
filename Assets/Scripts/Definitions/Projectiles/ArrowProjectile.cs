using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArrowProjectile : HomingProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 10;
            Velocity = new Vector3(0, 1f, 0);
            VelocityDampeningFactor = 0.2f;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());

            GameManager.Instance.SfxManager.AttachTrail("ArrowTrail", gameObject);
        }
    }
}