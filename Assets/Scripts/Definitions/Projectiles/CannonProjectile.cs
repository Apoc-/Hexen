using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class CannonProjectile : AoeProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 18;
            Radius = 1.5f;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();
            AddProjectileEffect(new DamageProjectileEffect());
        }
    }
}