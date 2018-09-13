using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArotasMeteoriteProjectile : AoeProjectile
    {
        protected override void InitProjectileData()
        {
            Speed = 20;
            Radius = 2.0f;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
            AddProjectileEffect(new DotProjectileEffect(damagePerTick: 2.5f, ticksPerSecond: 4, duration: 5));
            AddProjectileEffect(new StunProjectileEffect(stunDuration: 2));
        }
    }
}