﻿using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArchitectsBureauProjectile : DwarfsProjectile
    {
        protected override void InitProjectile()
        {
            base.InitProjectile();

            AddProjectileEffect(new DamageProjectileEffect());
        }
    }
}