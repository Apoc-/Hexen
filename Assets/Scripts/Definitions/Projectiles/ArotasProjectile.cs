using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArotasProjectile : ElvesProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 15;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
        }
    }
}