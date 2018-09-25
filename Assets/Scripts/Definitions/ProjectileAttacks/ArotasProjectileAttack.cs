using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArotasProjectileAttack : ProjectileAttack
    {
        protected override void InitAttackData()
        {
            
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();

            AddAttackEffect(new DamageAttackEffect());
        }
    }
}