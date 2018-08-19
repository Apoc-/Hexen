using System.Collections.Generic;
using Assets.Scripts.ProjectileSystem;
using UnityEngine;

namespace Hexen.GameData.Projectiles
{
    public class BoltProjectile : Projectile
    {
        protected override void InitProjectileData()
        {
            Speed = 20;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();
            AddProjectileEffect(new DamageProjectileEffect());
        }
    }
}