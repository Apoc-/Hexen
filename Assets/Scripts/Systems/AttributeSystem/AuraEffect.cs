namespace Systems.AttributeSystem
{
    public class AuraEffect
    {
        public AttributeEffect AttributeEffect;
        public bool AffectsNpcs;
        public bool AffectsTowers;

        public AuraEffect(AttributeEffect attributeEffect, bool affectsTowers, bool affectsNpcs)
        {
            AttributeEffect = attributeEffect;
            AffectsNpcs = affectsNpcs;
            AffectsTowers = affectsTowers;
        }
    }
}
