using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hexen
{
    public class Item : Entity
    {
        [SerializeField] private AttributeEffect[] attributeEffects;
        [SerializeField] private String name;
        [SerializeField] private String description;
    }
}
