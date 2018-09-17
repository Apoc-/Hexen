using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class CoinsProjectileEffect : ProjectileEffect
    {
        public CoinsProjectileEffect(float triggerChance) : base(triggerChance)
        {

        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            Debug.Log("Triggering Coins effect");
            source.Owner.IncreaseGold(1);
        }
    }
}