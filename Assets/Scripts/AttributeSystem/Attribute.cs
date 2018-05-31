using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using UnityEngine;

namespace Hexen
{
    public enum LevelIncrementType
    {
        Flat,
        Percentage
    }

    [Serializable]
    public class Attribute
    {
        #region static attribute names
        public static string AttackRange = "Attack Range";
        public static string AttackSpeed = "Attack Speed";
        public static string AttackDamage = "Attack Damage";
        #endregion

        private float levelIncrement = 0.0f;
        private LevelIncrementType levelIncrementType = LevelIncrementType.Flat;

        public string AttributeName;
        [SerializeField] private float baseValue;

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

        public Attribute(String attributeName, float baseValue, float levelIncrement, LevelIncrementType levelIncrementType) : this(attributeName, baseValue)
        {
            this.levelIncrement = levelIncrement;
            this.levelIncrementType = levelIncrementType;
        }

        public void AddAttributeEffect(AttributeEffect effect)
        {
            attributeEffects.Add(effect);
            isDirty = true;
        }

        public void RemoveAttributeEffect(AttributeEffect effect)
        {
            var e = attributeEffects[0];
            Debug.Log(e == effect);
            attributeEffects.Remove(effect);
            isDirty = true;
        }

        public void RemoveAllAttributeEffectsFromSource(AttributeEffectSource source)
        {
            var effectsFromSource = attributeEffects.Where(effect => effect.EffectSource == source).ToList();
            
            effectsFromSource.ForEach(RemoveAttributeEffect);
        }

        public void LevelUp()
        {
            switch (levelIncrementType)
            {
                case LevelIncrementType.Flat:
                    baseValue += levelIncrement;
                    break;
                case LevelIncrementType.Percentage:
                    baseValue *= (1 + levelIncrement);
                    break;
            }
        }

        public void LevelDown()
        {
            switch (levelIncrementType)
            {
                case LevelIncrementType.Flat:
                    baseValue -= levelIncrement;
                    break;
                case LevelIncrementType.Percentage:
                    baseValue /= (1 + levelIncrement);
                    break;
            }
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
