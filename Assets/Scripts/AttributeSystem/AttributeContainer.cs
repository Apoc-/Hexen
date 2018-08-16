using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hexen;
using Hexen.GameData.Towers;

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

    public bool ContainsAttribute(AttributeName attrName)
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

    public IEnumerator<KeyValuePair<AttributeName, Attribute>> GetEnumerator()
    {
        return attributes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}