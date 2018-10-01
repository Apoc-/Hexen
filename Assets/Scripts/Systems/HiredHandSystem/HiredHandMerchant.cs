using System.Collections.Generic;
using Assets.Scripts.Definitions.HiredHands;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.HandSystem
{
    public class HiredHandMerchant : Merchant
    {
        protected override void RegisterItems()
        {
            RegisterItem<Worker>();
        }
    }
}