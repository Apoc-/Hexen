using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.TowerSystem
{
    public abstract class Item : MonoBehaviour, AttributeEffectSource
    {
        private List<AttributeEffect> attributeEffects = new List<AttributeEffect>();
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
            attributeEffects.Add(effect);
        }
    }
}