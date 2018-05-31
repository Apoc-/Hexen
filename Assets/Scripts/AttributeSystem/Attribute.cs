using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using UnityEngine;

namespace Hexen
{
    [Serializable]
    public class Attribute
    {
        #region static attribute names
        public static string ATTACK_RANGE = "Attack Range";
        public static string ATTACK_SPEED = "Attack Speed";
        public static string ATTACK_DAMAGE = "Attack Damage";
        #endregion

        [SerializeField]
        public string attributeName;
        [SerializeField]
        private float baseValue;

        private float value;
        public float Value
        {
            get
            {
                if (isDirty)
                {
                    CalculateValue();
                }

                return value;
            }

            set
            {
                this.baseValue = value;
            }
        }

        private bool isDirty = true;

        private List<AttributeEffect> attributeEffects = new List<AttributeEffect>();

        public Attribute(String attributeName, float baseValue)
        {
            this.attributeName = attributeName;
            this.baseValue = baseValue;
        }

        public void AddAttributeEffect(AttributeEffect effect)
        {
            attributeEffects.Add(effect);
            isDirty = true;
        }

        public void RemoveAttributeEffect(AttributeEffect effect)
        {
            attributeEffects.Remove(effect);
            isDirty = true;
        }


        private void CalculateValue()
        {
            value = baseValue;
        }

    }
}
