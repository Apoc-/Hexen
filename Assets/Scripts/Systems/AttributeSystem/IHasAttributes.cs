using Hexen;
using Hexen.GameData.Towers;

namespace Hexen.AttributeSystem
{
    public interface IHasAttributes
    {
        void AddAttribute(Attribute attr);
        Attribute GetAttribute(AttributeName attrName);
        bool HasAttribute(AttributeName attrName);
    }
}