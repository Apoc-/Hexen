using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.SpecialEffectSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.AttackEffects
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