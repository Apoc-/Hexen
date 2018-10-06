using Systems.HiredHandSystem;
using UnityEngine;

namespace Systems.UiSystem.Core
{
    class UIManager : MonoBehaviour
    {
        [SerializeField] private BuildPanel buildPanel;
        [SerializeField] private TowerInfoPanel towerInfoPanel;
        [SerializeField] private FactionPanel factionPanel;
        [SerializeField] private CursorHandler cursorHandler;
        [SerializeField] private GameFinishedScreenBehaviour finishScreen;
        [SerializeField] private HiredHandPanel hiredHandPanel;

        public CursorHandler CursorHandler => cursorHandler;
        public BuildPanel BuildPanel => buildPanel;
        public TowerInfoPanel TowerInfoPanel => towerInfoPanel;
        public FactionPanel FactionPanel => factionPanel;
        public GameFinishedScreenBehaviour FinishScreen => finishScreen;
        public HiredHandPanel HiredHandPanel => hiredHandPanel;

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
