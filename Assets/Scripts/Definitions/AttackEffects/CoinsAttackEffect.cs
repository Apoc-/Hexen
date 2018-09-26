﻿using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class CoinsAttackEffect : AttackEffect
    {
        public CoinsAttackEffect(float triggerChance) : base(triggerChance)
        {

        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            source.PlayParticleEffectAboveTower("GotSomeCoinEffect", 3);
            source.Owner.IncreaseGold(1);
        }
    }
}