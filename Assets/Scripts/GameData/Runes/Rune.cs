using System.Collections.Generic;
using UnityEngine;

namespace Hexen.GameData.Runes
{
    public abstract class Rune : MonoBehaviour, AttributeEffectSource
    {
        private List<Tower> affectedTowers = new List<Tower>();
        private List<AttributeEffect> attributeEffects = new List<AttributeEffect>();        

        public virtual void InitRuneData()
        {

        }

        protected void AddAttributeEffect(AttributeEffect effect)
        {
            attributeEffects.Add(effect);
        }

        public void ApplyRune(Tower tower)
        {
            attributeEffects.ForEach(effect =>
            {
                var attr = effect.AffectedAttributeName;
                tower.GetAttribute(attr).AddAttributeEffect(effect);

                affectedTowers.Add(tower);
            });
        }

        public void RemoveRune()
        {
            affectedTowers.ForEach(tower =>
            {
                tower.Attributes.ForEach(attr =>
                {
                    attr.RemoveAllAttributeEffectsFromSource(this);
                });
            });
        }
    }
}