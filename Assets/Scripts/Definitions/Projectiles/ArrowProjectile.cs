using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArrowProjectile : HomingProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 30;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());

            GameManager.Instance.SfxManager.AttachTrail("ArrowTrail", gameObject);
        }
    }
}