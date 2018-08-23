using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArrowProjectile : Projectile
    {
        protected override void InitProjectileData()
        {
            Speed = 20;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
            //AddProjectileEffect(new DotProjectileEffect(200.0f, 10000f));
        }
    }
}