using System.Collections.Generic;
using Systems.AttributeSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Systems.ItemSystem
{
    public abstract class Item : MonoBehaviour, IAttributeEffectSource
    {
        private List<AttributeEffect> _attributeEffects = new List<AttributeEffect>();
        public Sprite Icon { get; set; }
        public int Cost { get; set; }
        public Rarities Rarity { get; set; }
        public string Name { get; set; }
        protected string Description { get; set; }

        public void InitItem()
        {
            InitData();
            InitAttributeEffects();
        }

        protected abstract void InitData();
        protected abstract void InitAttributeEffects();
        protected void AddAttributeEffect(AttributeEffect effect)
        {
            _attributeEffects.Add(effect);
        }
    }
}