using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class LaurinsVaultProjectile : DwarfsProjectile
    {
        protected override void InitProjectileEffects()
        {
            base.InitProjectileEffects();

            AddProjectileEffect(new LaurinsVaultProjectileEffect());
            AddProjectileEffect(new EnableRandomTowerProjectileEffect(0.05f));
        }
    }
}