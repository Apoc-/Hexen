using System;
using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.HandSystem
{
    public abstract class HiredHand : MonoBehaviour, AttributeEffectSource
    {
        protected Sprite Icon { get; set; }
        protected int Cost { get; set; }
        protected Rarities Rarity { get; set; }
        protected HiredHandType Type { get; set; }
        public string Name { get; set; }
        protected string Description { get; set; }

        private List<AttributeEffect> attributeEffects = new List<AttributeEffect>();

        public void InitHiredHand()
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