using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class VoidProjectile : Projectile
    {
        protected override void InitProjectileData()
        {
            
        }

        protected override void InitProjectileEffects()
        {
            ProjectileEffects = new List<ProjectileEffect>();

        }
    }
}