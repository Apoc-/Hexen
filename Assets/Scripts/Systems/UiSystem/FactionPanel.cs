using System.Collections.Generic;
using Systems.FactionSystem;
using Systems.GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Systems.UiSystem
{
    public class FactionPanel : MonoBehaviour
    {
        [FormerlySerializedAs("elvesButton")] [SerializeField] private Button _elvesButton;
        [FormerlySerializedAs("orcsButton")] [SerializeField] private Button _orcsButton;
        [FormerlySerializedAs("dwarfsButton")] [SerializeField] private Button _dwarfsButton;
        [FormerlySerializedAs("goblinsButton")] [SerializeField] private Button _goblinsButton;
        private Dictionary<FactionNames, Button> _factionButtons;

        public void Initialize()
        {
            _factionButtons = new Dictionary<FactionNames, Button>
            {
                { FactionNames.Elves, _elvesButton },
                { FactionNames.Orcs, _orcsButton },
                { FactionNames.Dwarfs, _dwarfsButton },
                { FactionNames.Goblins, _goblinsButton }
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
            var button = _factionButtons[factionName];
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

            if (!PlayerHasAmbassadors()) return;

            factionManager.SendAmbassador(factionName);

            //currently no upper limit
            /*if (factionManager.GetFactionByName(factionName).GetStanding() == 4)
            {
                DisableFactionButton(factionName);
            }*/
        }

        // ReSharper disable once UnusedMember.Local
        private void DisableFactionButton(FactionNames factionName)
        {
            var button = _factionButtons[factionName];
            button.interactable = false;
        }

        private bool PlayerHasAmbassadors()
        {
            return (GameManager.Instance.Player.GetAmbassadors() >= 1);
        }

        public void ToggleFactionWarButton(FactionNames factionName)
        {
            var button = _factionButtons[factionName];

            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
            button.gameObject.transform.Find("Icon/Badge/Swords").gameObject.SetActive(true);
        }
    }
}