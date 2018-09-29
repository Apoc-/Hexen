using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.WaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
{
    class UIManager : MonoBehaviour
    {
        [SerializeField] private BuildPanelBehaviour buildPanel;
        [SerializeField] private TowerInfoPanelBehaviour towerInfoPanel;
        [SerializeField] private FactionPanelBehaviour factionPanel;
        [SerializeField] private CursorHandler cursorHandler;
        [SerializeField] private GameFinishedScreenBehaviour finishScreen;


        public CursorHandler CursorHandler
        {
            get
            {
                return cursorHandler;
            }
        }

        public BuildPanelBehaviour BuildPanel
        {
            get
            {
                return buildPanel;
            }
        }

        public TowerInfoPanelBehaviour TowerInfoPanel
        {
            get
            {
                return towerInfoPanel;
            }
        }

        public FactionPanelBehaviour FactionPanel
        {
            get { return factionPanel; }
        }

        
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

        }

        public void InitializeUI()
        {
            FactionPanel.Initialize();
        }
    }
}
