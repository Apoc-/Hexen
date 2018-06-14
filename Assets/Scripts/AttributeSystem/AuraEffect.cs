using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;

namespace Hexen.AbilitySystem
{
    class AuraEffect
    {
        public AttributeEffect AttributeEffect;

        public AuraEffect(AttributeEffect attributeEffect)
        {
            AttributeEffect = attributeEffect;
        }
    }
}
