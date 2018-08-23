using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
{
    public class FactionPanelBehaviour : MonoBehaviour
    {
        [SerializeField] private Button elvesButton;
        [SerializeField] private Button orcsButton;
        [SerializeField] private Text ambassadorsLabel;
        private Dictionary<FactionNames, Button> factionButtons;

        public void Initialize()
        {
            factionButtons = new Dictionary<FactionNames, Button>
            {
                { FactionNames.Elves, elvesButton },
                { FactionNames.Orcs, orcsButton }
            };

            UpdateFactionButtons();
            UpdateAmbassadorsLabel();
        }

        public void UpdateFactionButtons()
        {
            UpdateFactionButton(this.elvesButton, FactionNames.Elves);
            UpdateFactionButton(this.orcsButton, FactionNames.Orcs);
        }

        public void UpdateAmbassadorsLabel()
        {
            var ambassadors = GameManager.Instance.Player.GetAmbassadors();
            ambassadorsLabel.text = "" + ambassadors;
        }

        private void UpdateFactionButton(Button button, FactionNames factionName)
        {
            var faction = GameManager.Instance.FactionManager.GetFactionByName(factionName);
            var text = button.GetComponentInChildren<Text>();

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

        public void HandleButtonClicked(FactionNames factionName)
        {
            var factionManager = GameManager.Instance.FactionManager;

            if (!playerHasAmbassadors()) return;

            factionManager.SendAmbassador(factionName);
        }

        private bool playerHasAmbassadors()
        {
            return (GameManager.Instance.Player.GetAmbassadors() >= 1);
        }

        public void ToggleFactionWarButton(FactionNames factionName)
        {
            var button = factionButtons[factionName];

            button.interactable = false;
            button.GetComponentInChildren<Text>().gameObject.SetActive(false);
            button.gameObject.transform.Find("Badge/Swords").gameObject.SetActive(true);
        }
    }
}