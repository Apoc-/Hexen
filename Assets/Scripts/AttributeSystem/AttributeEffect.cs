using Hexen;

namespace Hexen
{
    public enum AttributeEffectType
    {
        Flat,
        PercentMul,
        PercentAdd
    }

    public class AttributeEffect
    {
        public float Value { get; private set; }
        public AttributeEffectType EffectType { get; private set; }

        public AttributeEffect(float value, AttributeEffectType effectType)
        {
            Value = value;
            EffectType = effectType;
        }
    }
}
