using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems.AttributeSystem
{
    public enum AttributeEffectType
    {
        Flat,
        PercentMul,
        PercentAdd
    }

    [Serializable]
    public class AttributeEffect
    {
        [SerializeField] private AttributeName affectedAttributeName;

        public AttributeName AffectedAttributeName
        {
            get { return affectedAttributeName; }
            private set { affectedAttributeName = value; }
        }

        public float Value { get; private set; }
        public AttributeEffectType EffectType { get; private set; }

        public AttributeEffectSource EffectSource { get; private set; }

        public float AppliedTimestamp { get; private set; }
        public int Duration { get; private set; }

        public AttributeEffect(float value, AttributeName affectedAttributeName, AttributeEffectType effectType, AttributeEffectSource effectSource, int duration = -1)
        {
            Value = value;
            EffectType = effectType;
            EffectSource = effectSource;
            AffectedAttributeName = affectedAttributeName;
            AppliedTimestamp = Time.time;
            Duration = duration;
        }

        public AttributeEffect(AttributeEffect source)
        {
            Value = source.Value;
            EffectType = source.EffectType;
            EffectSource = source.EffectSource;
            AffectedAttributeName = source.AffectedAttributeName;
            Duration = source.Duration;

            AppliedTimestamp = Time.time;
            
        }

        private sealed class ValueEffectTypeEffectSourceEqualityComparer : IEqualityComparer<AttributeEffect>
        {
            public bool Equals(AttributeEffect x, AttributeEffect y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Value.Equals(y.Value) && x.EffectType == y.EffectType && Equals(x.EffectSource, y.EffectSource);
            }

            public int GetHashCode(AttributeEffect obj)
            {
                unchecked
                {
                    var hashCode = obj.Value.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int) obj.EffectType;
                    hashCode = (hashCode * 397) ^ (obj.EffectSource != null ? obj.EffectSource.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var effect = obj as AttributeEffect;
            return effect != null &&
                   Value == effect.Value &&
                   EffectType == effect.EffectType &&
                   EqualityComparer<AttributeEffectSource>.Default.Equals(EffectSource, effect.EffectSource);
        }

        public override int GetHashCode()
        {
            var hashCode = -321209641;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + EffectType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<AttributeEffectSource>.Default.GetHashCode(EffectSource);
            return hashCode;
        }
    }
}
