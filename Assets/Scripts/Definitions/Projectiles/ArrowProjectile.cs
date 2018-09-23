using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArrowProjectile : Projectile
    {
        protected override void InitProjectileData()
        {
           
        }

        protected override void InitProjectileEffects()
        {
            ProjectileEffects = new List<ProjectileEffect>();
            AddProjectileEffect(new DamageProjectileEffect());

            GameManager.Instance.SfxManager.AttachTrail("ArrowTrail", gameObject);
        }
    }
}