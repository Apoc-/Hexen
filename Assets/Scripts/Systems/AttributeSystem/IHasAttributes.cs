namespace Assets.Scripts.Systems.AttributeSystem
{
    public interface IHasAttributes
    {
        void AddAttribute(Attribute attr);
        Attribute GetAttribute(AttributeName attrName);
        bool HasAttribute(AttributeName attrName);
    }
}