using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class OrcAoeProjectile : AoeProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 20;
            Radius = 1f;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
            AddProjectileEffect(new FrenzyProjectileEffect());
        }
    }
}