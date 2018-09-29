using System.Collections.Generic;
using Assets.Scripts.Definitions.HiredHands;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.HandSystem
{
    public class HiredHandManager : MonoBehaviour
    {
        
        private Dictionary<Rarities, List<HiredHand>> hiredHands = new Dictionary<Rarities, List<HiredHand>>
        {
            { Rarities.Common, new List<HiredHand>() },
            { Rarities.Uncommon, new List<HiredHand>() },
            { Rarities.Rare, new List<HiredHand>() },
            { Rarities.Legendary, new List<HiredHand>() }
        };

        public void Awake()
        {
            RegisterHiredHands();
        }

        private void RegisterHiredHands()
        {
            RegisterHiredHand<Worker>();
        }

        private void RegisterHiredHand<T>() where T : HiredHand
        {
            GameObject go = new GameObject();
            HiredHand hand = go.AddComponent<T>();

            hand.InitHiredHand();

            go.name = hand.Name;
            go.transform.parent = transform;
            go.SetActive(false);
        }

        public List<HiredHand> GetHiredHandsOfRarity(Rarities rarity)
        {
            return hiredHands[rarity];
        }
    }
}