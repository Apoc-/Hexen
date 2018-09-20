using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
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
            var dmg = 0f;
            if (source.HasAttribute(AttributeName.AttackDamage))
            {
                dmg = source.Attributes.GetAttribute(AttributeName.AttackDamage).Value;
            }


            var pos = target.transform.position;
            pos.y += target.transform.lossyScale.y;
            GameManager.Instance.SfxManager.PlayTextEffect((int)(2*dmg) + "!", pos, 2, 2.0f, GameSettings.MagicalCritColor); 

            target.DealDamage(dmg, source);
        }
    }
}