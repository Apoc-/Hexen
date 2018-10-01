using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
{
    public class FactionPanel : MonoBehaviour
    {
        [SerializeField] private Button elvesButton;
        [SerializeField] private Button orcsButton;
        [SerializeField] private Button dwarfsButton;
        [SerializeField] private Button goblinsButton;
        private Dictionary<FactionNames, Button> factionButtons;

        public void Initialize()
        {
            factionButtons = new Dictionary<FactionNames, Button>
            {
                { FactionNames.Elves, elvesButton },
                { FactionNames.Orcs, orcsButton },
                { FactionNames.Dwarfs, dwarfsButton },
                { FactionNames.Goblins, goblinsButton }
            };

            UpdateFactionButtons();
        }

        public void UpdateFactionButtons()
        {
            UpdateFactionButton(FactionNames.Elves);
            UpdateFactionButton(FactionNames.Orcs);
            UpdateFactionButton(FactionNames.Dwarfs);
            UpdateFactionButton(FactionNames.Goblins);
        }

        private void UpdateFactionButton(FactionNames factionName)
        {
            var faction = GameManager.Instance.FactionManager.GetFactionByName(factionName);
            var button = factionButtons[factionName];
            var text = button.GetComponentInChildren<TextMeshProUGUI>();

            if (text != null)
            {
                text.text = "" + faction.GetStanding();
            }
        }

        public void OnElvesButtonClicked()
        {
            HandleButtonClicked(FactionNames.Elves);
        }

        public void OnOrcsButtonClicked()
        {
            HandleButtonClicked(FactionNames.Orcs);
        }

        public void OnDwarfsButtonClicked()
        {
            HandleButtonClicked(FactionNames.Dwarfs);
        }

        public void OnGoblinsButtonClicked()
        {
            HandleButtonClicked(FactionNames.Goblins);
        }

        public void HandleButtonClicked(FactionNames factionName)
        {
            var factionManager = GameManager.Instance.FactionManager;

            if (!playerHasAmbassadors()) return;

            factionManager.SendAmbassador(factionName);

            //currently no upper limit
            /*if (factionManager.GetFactionByName(factionName).GetStanding() == 4)
            {
                DisableFactionButton(factionName);
            }*/
        }

        private void DisableFactionButton(FactionNames factionName)
        {
            var button = factionButtons[factionName];
            button.interactable = false;
        }

        private bool playerHasAmbassadors()
        {
            return (GameManager.Instance.Player.GetAmbassadors() >= 1);
        }

        public void ToggleFactionWarButton(FactionNames factionName)
        {
            var button = factionButtons[factionName];

            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
            button.gameObject.transform.Find("Icon/Badge/Swords").gameObject.SetActive(true);
        }
    }
}