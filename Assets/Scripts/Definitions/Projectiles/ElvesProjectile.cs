using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ElvesProjectile : HomingProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 12;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
            AddProjectileEffect(new MagicalAffinityProjectileEffect(0.1f));
        }
    }
}