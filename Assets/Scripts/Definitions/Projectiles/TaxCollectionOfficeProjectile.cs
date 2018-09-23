using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class TaxCollectionOfficeProjectile : DwarfsProjectile
    {
        protected override void InitProjectileEffects()
        {
            base.InitProjectileEffects();

            AddProjectileEffect(new DamageProjectileEffect());
        }
    }
}