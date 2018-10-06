using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.AttributeSystem
{
    public enum AttributeEffectType
    {
        Flat,
        PercentMul,
        PercentAdd,
        SetValue
    }

    [Serializable]
    public class AttributeEffect
    {
        private AttributeName _affectedAttributeName;

        public AttributeName AffectedAttributeName
        {
            get => _affectedAttributeName;
            private set => _affectedAttributeName = value;
        }

        public float Value { get; }
        public AttributeEffectType EffectType { get; }
        public IAttributeEffectSource EffectSource { get; }
        public float AppliedTimestamp { get; }
        public float Duration { get; }
        public Action<Attribute> FinishedCallback { get; }

        public AttributeEffect(
            float value, 
            AttributeName affectedAttributeName, 
            AttributeEffectType effectType, 
            IAttributeEffectSource effectSource, 
            float duration = -1.0f, 
            Action<Attribute> finishedCallback = null)
        {
            Value = value;
            EffectType = effectType;
            EffectSource = effectSource;
            AffectedAttributeName = affectedAttributeName;
            AppliedTimestamp = Time.time;
            Duration = duration;
            FinishedCallback = finishedCallback;
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

        // ReSharper disable once UnusedMember.Local
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
            return obj is AttributeEffect effect &&
                   Math.Abs(Value - effect.Value) < 1e-10000 &&
                   EffectType == effect.EffectType &&
                   EqualityComparer<IAttributeEffectSource>.Default.Equals(EffectSource, effect.EffectSource);
        }

        public override int GetHashCode()
        {
            var hashCode = -321209641;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + EffectType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IAttributeEffectSource>.Default.GetHashCode(EffectSource);
            return hashCode;
        }
    }
}
