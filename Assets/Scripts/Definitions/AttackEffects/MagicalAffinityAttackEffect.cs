using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class MagicalAffinityAttackEffect : AttackEffect
    {
        public MagicalAffinityAttackEffect(float triggerChance) : base(triggerChance)
        {
        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            var dmg = 0f;
            if (source.HasAttribute(AttributeName.AttackDamage))
            {
                dmg = source.Attributes.GetAttribute(AttributeName.AttackDamage).Value;
            }

            var offset = new Vector3(0, target.transform.lossyScale.y, 0);
            var effectText = (int) (2 * dmg) + "!";
            var textSize = 2;
            var textDuration = 2.0f;
            var textEffect = 
                new TextEffectData(effectText,textSize, GameSettings.MagicalCritColor, target.gameObject,offset,textDuration);

            GameManager.Instance.SpecialEffectManager.PlayTextEffect(textEffect);

            target.DealDamage(dmg, source);
        }
    }
}