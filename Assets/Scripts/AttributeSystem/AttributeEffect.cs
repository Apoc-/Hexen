using Hexen;

namespace Hexen
{
    public enum AttributeEffectType
    {
        Flat,
        Percent
    }

    public class AttributeEffect
    {
        public float Value { get; private set; }
        public AttributeEffectType EffectType { get; private set; }
    }
}
