using System;
using System.Collections.Generic;
using System.Linq;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
{
    [Serializable]
    public enum LevelIncrementType
    {
        Flat,
        Percentage
    }

    [Serializable]
    public class Attribute : AttributeEffectSource
    {
        [SerializeField] private List<AttributeEffect> attributeEffects;

        private Dictionary<int, AttributeEffect> levelAttributeEffects;
        private Dictionary<int, AttributeEffect> LevelAttributeEffects
        {
            get { return levelAttributeEffects ?? (levelAttributeEffects = new Dictionary<int, AttributeEffect>()); }
        }

        private int attributeLevel;
        public AttributeName AttributeName;
        [SerializeField] private float baseValue;
        [SerializeField] private bool isDirty;

        public float LevelIncrement;
        public LevelIncrementType LevelIncrementType;
        
        public Attribute(AttributeName attributeName, float baseValue, float levelIncrement,
            LevelIncrementType levelIncrementType)
        {
            LevelIncrement = levelIncrement;
            LevelIncrementType = levelIncrementType;
            AttributeName = attributeName;
            this.baseValue = baseValue;
            attributeLevel = 1;
            isDirty = true;
           

            attributeEffects = new List<AttributeEffect>();
            levelAttributeEffects = new Dictionary<int, AttributeEffect>();
        }

        private float value;
        public float Value
        {
            get
            {
                if (isDirty) value = CalculateValue();

                return value;
            }

            set { baseValue = value; }
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

        public void RemoveAllAttributeEffectsFromSource(AttributeEffectSource source)
        {
            var effectsFromSource = attributeEffects.Where(effect => effect.EffectSource == source).ToList();

            effectsFromSource.ForEach(RemoveAttributeEffect);
        }

        public void LevelUp()
        {
            attributeLevel += 1;

            if (LevelIncrementType == LevelIncrementType.Flat)
            {
                var levelEffect = new AttributeEffect(LevelIncrement, AttributeName, AttributeEffectType.Flat, this);
                LevelAttributeEffects.Add(attributeLevel, levelEffect);
            }
            else
            {
                var levelEffect = new AttributeEffect(LevelIncrement, AttributeName, AttributeEffectType.PercentMul, this);
                LevelAttributeEffects.Add(attributeLevel, levelEffect);
            }

            isDirty = true;
        }

        public void LevelDown()
        {
            LevelAttributeEffects.Remove(attributeLevel);
            attributeLevel -= 1;
            isDirty = true;
        }

        private float CalculateValue()
        {
            var calcValue = baseValue;
            var addPercBonusSum = 0.0f;

            var effects = new List<AttributeEffect>();

            if (attributeEffects.Count > 0) effects.AddRange(attributeEffects);
            if (LevelAttributeEffects.Count > 0) effects.AddRange(LevelAttributeEffects.Values);

            effects.ForEach(effect =>
            {
                switch (effect.EffectType)
                {
                    case AttributeEffectType.Flat:
                        calcValue += effect.Value;
                        break;
                    case AttributeEffectType.PercentMul:
                        calcValue *= 1 + effect.Value;
                        break;
                    case AttributeEffectType.PercentAdd:
                        addPercBonusSum += effect.Value;
                        break;
                }
            });

            //finally apply additive percentage bonusses
            calcValue *= 1 + addPercBonusSum;
            isDirty = false;

            return calcValue;
        }
    }
}