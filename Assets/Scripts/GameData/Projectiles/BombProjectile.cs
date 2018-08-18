using System.Collections.Generic;
using Assets.Scripts.ProjectileSystem;
using UnityEngine;

namespace Hexen.GameData.Projectiles
{
    public class CannonProjectile : AoeProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 18;
            Radius = 2.5f;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();
            AddProjectileEffect(new DamageProjectileEffect());
        }
    }
}