using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Systems.AttributeSystem
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
        private List<AttributeEffect> attributeEffects;

        private Dictionary<int, AttributeEffect> levelAttributeEffects;
        private Dictionary<int, AttributeEffect> LevelAttributeEffects
        {
            get { return levelAttributeEffects ?? (levelAttributeEffects = new Dictionary<int, AttributeEffect>()); }
        }

        protected int AttributeLevel;
        public AttributeName AttributeName;
        protected float baseValue;
        protected bool isDirty;

        public float LevelIncrement;
        public LevelIncrementType LevelIncrementType;

        public Attribute(
            AttributeName attributeName, 
            float baseValue, 
            float levelIncrement = 0.25f,
            LevelIncrementType levelIncrementType = LevelIncrementType.Percentage)
        {
            LevelIncrement = levelIncrement;
            LevelIncrementType = levelIncrementType;
            AttributeName = attributeName;
            this.baseValue = baseValue;
            AttributeLevel = 1;
            isDirty = true;
           

            attributeEffects = new List<AttributeEffect>();
            levelAttributeEffects = new Dictionary<int, AttributeEffect>();
        }

        public Attribute(Attribute source)
        {
            LevelIncrement = source.LevelIncrement;
            LevelIncrementType = source.LevelIncrementType;
            AttributeName = source.AttributeName;
            AttributeLevel = 1; //is leveled up by tower levelup
            this.baseValue = source.baseValue;
            isDirty = true;

            attributeEffects = new List<AttributeEffect>();

            source.attributeEffects.ForEach(effect =>
            {
                attributeEffects.Add(new AttributeEffect(effect));
            });
            
            levelAttributeEffects = new Dictionary<int, AttributeEffect>();
        }

        protected float value;
        public virtual float Value
        {
            get
            {
                if (isDirty) value = CalculateValue();

                return value;
            }

            set
            {
                baseValue = value;
                isDirty = true;
            }
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

        public void RemoveAttributeEffectsFromSource(AttributeEffectSource source)
        {
            attributeEffects
                .Where(effect => effect.EffectSource == source)
                .ToList()
                .ForEach(RemoveAttributeEffect);
        }

        public void LevelUp()
        {
            AttributeLevel += 1;

            if (LevelIncrementType == LevelIncrementType.Flat)
            {
                var levelEffect = new AttributeEffect(LevelIncrement, AttributeName, AttributeEffectType.Flat, this);
                LevelAttributeEffects.Add(AttributeLevel, levelEffect);
            }
            else
            {
                var levelEffect = new AttributeEffect(LevelIncrement, AttributeName, AttributeEffectType.PercentMul, this);
                LevelAttributeEffects.Add(AttributeLevel, levelEffect);
            }

            isDirty = true;
        }

        public void LevelDown()
        {
            AttributeLevel -= 1;
            LevelAttributeEffects.Remove(AttributeLevel);
            isDirty = true;
        }

        protected virtual float CalculateValue()
        {
            var calcValue = baseValue;
            var addPercBonusSum = 0.0f;

            var effects = new List<AttributeEffect>();

            if (attributeEffects.Count > 0) effects.AddRange(attributeEffects);

            //check of SetValue Effect
            var setValueEffect = effects.FirstOrDefault(effect => effect.EffectType == AttributeEffectType.SetValue);
            if (setValueEffect != null)
            {
                return setValueEffect.Value;
            }

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

        public void RemovedFinishedAttributeEffects()
        {
            var finishedEffects = this.attributeEffects
                .Where(effect => effect.Duration > 0)
                .Where(effect => Time.time - effect.AppliedTimestamp >= effect.Duration)
                .ToList();

            finishedEffects.ForEach(effect =>
            {
                RemoveAttributeEffect(effect);
                if (effect.FinishedCallback != null) effect.FinishedCallback(this);
            });
        }
    }
}