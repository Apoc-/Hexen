using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArotasMeteoriteProjectileAttack : ProjectileAttack
    {
        public float DmgPerTick { get; set; }
        public float TicksPerSecond { get; set; }
        public float Duration { get; set; }

        protected override void InitAttackData()
        {
            SplashRadius = 1.75f;
            FlightDuration = 1.5f;

            var fireTrail = new ParticleEffectData("FireTrail", gameObject, 10, true);
            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(fireTrail);
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();

            AddAttackEffect(new DamageAttackEffect());
            AddAttackEffect(new DotAttackEffect(DmgPerTick, TicksPerSecond, Duration));
            AddAttackEffect(new StunAttackEffect(stunDuration: 2));
        }
    }
}