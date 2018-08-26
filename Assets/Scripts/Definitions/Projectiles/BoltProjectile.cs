using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class BoltProjectile : HomingProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 20;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
            AddProjectileEffect(new SlowProjectileEffect(0.10f));
        }
    }
}