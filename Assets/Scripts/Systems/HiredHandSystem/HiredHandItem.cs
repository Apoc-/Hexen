using System;
using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.HandSystem
{
    public abstract class HiredHandItem : Item
    {
        protected HiredHandType Type { get; set; }
        
    }
}