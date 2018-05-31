using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using UnityEngine;

namespace Hexen
{
    [Serializable]
    public class Attribute
    {
        #region static attribute names
        public static string AttackRange = "Attack Range";
        public static string AttackSpeed = "Attack Speed";
        public static string AttackDamage = "Attack Damage";
        #endregion

        public string AttributeName;
        [SerializeField]
        private float baseValue;

        private float value;
        public float Value
        {
            get
            {
                if (isDirty)
                {
                    value = CalculateValue();
                }

                return value;
            }

            set
            {
                this.baseValue = value;
            }
        }

        private bool isDirty = true;

        private List<AttributeEffect> attributeEffects = new List<AttributeEffect>();

        public Attribute(String attributeName, float baseValue)
        {
            this.AttributeName = attributeName;
            this.baseValue = baseValue;
        }

        public void AddAttributeEffect(AttributeEffect effect)
        {
            attributeEffects.Add(effect);
            isDirty = true;
        }

        public void RemoveAttributeEffect(AttributeEffect effect)
        {
            attributeEffects.Remove(effect);
            isDirty = true;
        }


        private float CalculateValue()
        {
            var calcValue = baseValue;
            var addPercBonusSum = 0.0f;

            attributeEffects.ForEach(effect =>
            {
                switch (effect.EffectType)
                {
                    case AttributeEffectType.Flat:
                        calcValue += effect.Value;
                        break;
                    case AttributeEffectType.PercentMul:
                        calcValue *= (1 + effect.Value);
                        break;
                    case AttributeEffectType.PercentAdd:
                        addPercBonusSum += effect.Value;
                        break;
                }
            });

            //finally apply additive percentage bonusses
            calcValue *= (1 + addPercBonusSum);

            return calcValue;
        }   
    }
}
