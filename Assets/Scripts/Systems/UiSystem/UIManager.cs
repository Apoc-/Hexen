using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.WaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
{
    class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject goldInfo;
        [SerializeField] private GameObject lifeInfo;
        [SerializeField] private GameObject waveInfo;
        [SerializeField] private GameObject waveTimer;
        [SerializeField] private BuildPanelBehaviour buildPanel;
        [SerializeField] private InfoPopupBehaviour infoPopup;
        [SerializeField] private FactionPanelBehaviour factionPanel;

        public BuildPanelBehaviour BuildPanel
        {
            get
            {
                return buildPanel;
            }
        }

        public InfoPopupBehaviour InfoPopup
        {
            get
            {
                return infoPopup;
            }
        }

        public FactionPanelBehaviour FactionPanel
        {
            get { return factionPanel; }
        }

        [SerializeField] private GameFinishedScreenBehaviour finishScreen;
        public GameFinishedScreenBehaviour FinishScreen
        {
            get
            {
                return finishScreen;
            }
        }

        public void Update()
        {
            UpdateInfoPanels();
        }

        private void UpdateInfoPanels()
        {
            goldInfo.GetComponentInChildren<Text>().text = "" + GameManager.Instance.Player.Gold;
            lifeInfo.GetComponentInChildren<Text>().text = "" + GameManager.Instance.Player.Lives;

            var spawner = GameManager.Instance.WaveSpawner;
            var displayTime = spawner.WaveCooldown - spawner.CurrentElapsedTime;
            waveTimer.GetComponentInChildren<Text>().text = "" + displayTime;

            var currentWave = GameManager.Instance.WaveSpawner.CurrentWave;
            var totalWaves = WaveProvider.WaveCount;
            waveInfo.GetComponentInChildren<Text>().text = currentWave + "/" + totalWaves;
        }

        public void InitializeUI()
        {
            FactionPanel.Initialize();
        }
    }
}
