using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArotasMeteoriteProjectile : Projectile
    {
        public float DmgPerTick { get; set; }
        public float TicksPerSecond { get; set; }
        public float Duration { get; set; }

        protected override void InitProjectileData()
        {
            SplashRadius = 1.75f;
            FlightDuration = 1.5f;

            var fireTrail = new SpecialEffect("FireTrail", gameObject, 10, true);
            GameManager.Instance.SfxManager.PlaySpecialEffect(fireTrail);
        }

        protected override void InitProjectileEffects()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
            AddProjectileEffect(new DotProjectileEffect(DmgPerTick, TicksPerSecond, Duration));
            AddProjectileEffect(new StunProjectileEffect(stunDuration: 2));
        }
    }
}