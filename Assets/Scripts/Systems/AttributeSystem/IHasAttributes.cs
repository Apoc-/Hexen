namespace Assets.Scripts.Systems.AttributeSystem
{
    public interface IHasAttributes
    {
        void AddAttribute(Attribute attr);
        Attribute GetAttribute(AttributeName attrName);

        float GetAttributeValue(AttributeName attrName);
        bool HasAttribute(AttributeName attrName);
        void RemoveFinishedTimedAttributeEffects();
    }
}