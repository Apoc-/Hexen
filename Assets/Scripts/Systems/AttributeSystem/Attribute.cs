using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systems.AttributeSystem
{
    [Serializable]
    public enum LevelIncrementType
    {
        Flat,
        Percentage
    }

    [Serializable]
    public class Attribute : IAttributeEffectSource
    {
        private readonly List<AttributeEffect> _attributeEffects;

        private Dictionary<int, AttributeEffect> _levelAttributeEffects;
        private Dictionary<int, AttributeEffect> LevelAttributeEffects => _levelAttributeEffects 
               ?? (_levelAttributeEffects = new Dictionary<int, AttributeEffect>());

        private int _attributeLevel;
        public AttributeName AttributeName;
        protected float BaseValue;
        protected bool IsDirty;

        public float LevelIncrement;
        public LevelIncrementType LevelIncrementType;

        public Attribute(
            AttributeName attributeName, 
            float baseValue, 
            float levelIncrement = 0.0f,
            LevelIncrementType levelIncrementType = LevelIncrementType.Percentage)
        {
            LevelIncrement = levelIncrement;
            LevelIncrementType = levelIncrementType;
            AttributeName = attributeName;
            BaseValue = baseValue;
            _attributeLevel = 1;
            IsDirty = true;
           

            _attributeEffects = new List<AttributeEffect>();
            _levelAttributeEffects = new Dictionary<int, AttributeEffect>();
        }

        public Attribute(Attribute source)
        {
            LevelIncrement = source.LevelIncrement;
            LevelIncrementType = source.LevelIncrementType;
            AttributeName = source.AttributeName;
            _attributeLevel = 1; //is leveled up by tower levelup
            BaseValue = source.BaseValue;
            IsDirty = true;

            _attributeEffects = new List<AttributeEffect>();

            source._attributeEffects.ForEach(effect =>
            {
                _attributeEffects.Add(new AttributeEffect(effect));
            });
            
            _levelAttributeEffects = new Dictionary<int, AttributeEffect>();
        }

        // ReSharper disable once InconsistentNaming
        protected float value;
        public virtual float Value
        {
            get
            {
                if (IsDirty) value = CalculateValue();

                return value;
            }

            set
            {
                BaseValue = value;
                IsDirty = true;
            }
        }

        public void AddAttributeEffect(AttributeEffect effect)
        {
            _attributeEffects.Add(effect);
            IsDirty = true;
        }

        private void RemoveAttributeEffect(AttributeEffect effect)
        {
            _attributeEffects.Remove(effect);
            IsDirty = true;
        }

        public void RemoveAttributeEffectsFromSource(IAttributeEffectSource source)
        {
            _attributeEffects
                .Where(effect => effect.EffectSource == source)
                .ToList()
                .ForEach(RemoveAttributeEffect);
        }

        public void LevelUp()
        {
            _attributeLevel += 1;

            if (LevelIncrementType == LevelIncrementType.Flat)
            {
                var levelEffect = new AttributeEffect(LevelIncrement, AttributeName, AttributeEffectType.Flat, this);
                LevelAttributeEffects.Add(_attributeLevel, levelEffect);
            }
            else
            {
                var levelEffect = new AttributeEffect(LevelIncrement, AttributeName, AttributeEffectType.PercentMul, this);
                LevelAttributeEffects.Add(_attributeLevel, levelEffect);
            }

            IsDirty = true;
        }

        public void LevelDown()
        {
            _attributeLevel -= 1;
            LevelAttributeEffects.Remove(_attributeLevel);
            IsDirty = true;
        }

        protected virtual float CalculateValue()
        {
            var calcValue = BaseValue;
            var addPercBonusSum = 0.0f;

            var effects = new List<AttributeEffect>();

            if (_attributeEffects.Count > 0) effects.AddRange(_attributeEffects);

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
            IsDirty = false;

            return calcValue;
        }

        public void RemovedFinishedAttributeEffects()
        {
            var finishedEffects = _attributeEffects
                .Where(effect => effect.Duration > 0)
                .Where(effect => Time.time - effect.AppliedTimestamp >= effect.Duration)
                .ToList();

            finishedEffects.ForEach(effect =>
            {
                RemoveAttributeEffect(effect);
                effect.FinishedCallback?.Invoke(this);
            });
        }
    }
}