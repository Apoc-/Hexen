using System.Collections.Generic;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.HandSystem
{
    public class HiredHandPanel : MonoBehaviour
    {
        private List<HiredHandButton> selectedHiredHands = new List<HiredHandButton>();
        [SerializeField] private GameObject availableHiredHandsContainer;

        public void SelectHiredHand(HiredHandButton hand)
        {
            selectedHiredHands.Add(hand);
        }

        public void DeselectHiredHand(HiredHandButton hand)
        {
            selectedHiredHands.Remove(hand);
        }

        //todo
        public void AddHiredHandButton()
        {
            HiredHandButton button = Instantiate(Resources.Load<HiredHandButton>("Prefabs/UI/HiredHandButton"));
            var hand = GameManager.Instance.HiredHandManager.GetHiredHandsOfRarity(Rarities.Common)[0];

            button.Hand = hand;
            button.transform.parent = availableHiredHandsContainer.transform;
        }
    }
}