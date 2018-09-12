using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class MagicalAffinityProjectileEffect : ProjectileEffect
    {
        public MagicalAffinityProjectileEffect(float triggerChance) : base(triggerChance)
        {
        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            Debug.Log("Triggering Magical Affinity");
            var dmg = 0f;
            if (source.HasAttribute(AttributeName.AttackDamage))
            {
                dmg = source.Attributes.GetAttribute(AttributeName.AttackDamage).Value;
            }

            target.DealDamage(dmg, source);
        }
    }
}