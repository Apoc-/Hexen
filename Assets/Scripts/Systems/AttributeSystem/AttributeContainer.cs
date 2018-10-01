using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Systems.AttributeSystem
{
    public class AttributeContainer : IEnumerable<KeyValuePair<AttributeName, Attribute>>
    {
        private Dictionary<AttributeName, Attribute> attributes = new Dictionary<AttributeName, Attribute>();

        // Allow this[name]
        public Attribute this[AttributeName attrName]
        {
            get { return GetAttribute(attrName); }
        }

        public Attribute GetAttribute(AttributeName attrName)
        {
            return attributes[attrName];
        }

        public void AddAttribute(Attribute attr)
        {
            attributes.Add(attr.AttributeName, attr);
        }

        public bool HasAttribute(AttributeName attrName)
        {
            return attributes.ContainsKey(attrName);
        }

        public void RemoveAttributeEffectsFromSource(AttributeEffectSource source)
        {
            foreach (var keyValuePair in attributes)
            {
                keyValuePair.Value.RemoveAttributeEffectsFromSource(source);
            }
        }

        public int Count()
        {
            return attributes.Count;
        }

        public IEnumerator<KeyValuePair<AttributeName, Attribute>> GetEnumerator()
        {
            return attributes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}