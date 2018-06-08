using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hexen
{
    public class Item : Entity, AttributeEffectSource
    {
        [SerializeField] private List<AttributeEffect> attributeEffects;
        [SerializeField] private String name;
        [SerializeField] private String description;
        [SerializeField] private Sprite icon;

        public void OnEnable()
        {
            //attributeEffects.Add(new AttributeEffect(Attribute.AttackRange, 1, AttributeEffectType.Flat, this));
        }
    }
}
