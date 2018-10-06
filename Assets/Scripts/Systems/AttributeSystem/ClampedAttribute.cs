namespace Systems.AttributeSystem
{
    public class ClampedAttribute : Attribute
    {
        private float minValue;
        private float maxValue;

        public override float Value
        {
            get
            {
                if (IsDirty) value = CalculateValue();

                return value;
            }

            set
            {
                BaseValue = value;

                if (BaseValue < minValue) BaseValue = minValue;
                if (BaseValue > maxValue) BaseValue = maxValue;

                IsDirty = true;
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
            maxValue = source.maxValue;
            minValue = source.minValue;
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