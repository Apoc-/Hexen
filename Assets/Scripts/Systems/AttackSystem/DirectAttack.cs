using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class DirectAttack : AbstractAttack
    {
        public override void InitAttack(Npc target, Tower source)
        {
            base.InitAttack(target, source);

            ExecuteAttack();
        }

        protected virtual void ExecuteAttack()
        {
            ApplyEffectsToTarget(Target);
        }
    }
}