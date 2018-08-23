using Hexen;
using Hexen.GameData.Towers;

namespace Assets.Scripts.AttributeSystem
{
    public class ClampedAttribute : Attribute
    {
        private float minValue;
        private float maxValue;

        public override float Value
        {
            get
            {
                if (isDirty) value = CalculateValue();

                return value;
            }

            set
            {
                baseValue = value;

                if (baseValue < minValue) baseValue = minValue;
                if (baseValue > maxValue) baseValue = maxValue;

                isDirty = true;
            }
        }

        public ClampedAttribute(AttributeName attributeName, float baseValue, float minValue, float maxValue,
            float levelIncrement, LevelIncrementType levelIncrementType) : base(attributeName, baseValue,
            levelIncrement, levelIncrementType)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public ClampedAttribute(ClampedAttribute source) : base(source)
        {
            this.maxValue = source.maxValue;
            this.minValue = source.minValue;
        }

        protected override float CalculateValue()
        {
            var val = base.CalculateValue();

            if (val > maxValue) val = maxValue;
            if (val < minValue) val = minValue;

            return val;
        }
    }
}