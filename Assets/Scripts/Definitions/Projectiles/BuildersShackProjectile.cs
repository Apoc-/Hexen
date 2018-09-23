using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class BuildersShackProjectile : DwarfsProjectile
    {
        protected override void InitProjectileEffects()
        {
            base.InitProjectileEffects();

            AddProjectileEffect(new DamageProjectileEffect());
        }
    }
}