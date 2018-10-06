using Systems.HiredHandSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.UiSystem.Core
{
    class UIManager : MonoBehaviour
    {
        [FormerlySerializedAs("buildPanel")] [SerializeField] private BuildPanel _buildPanel;
        [FormerlySerializedAs("towerInfoPanel")] [SerializeField] private TowerInfoPanel _towerInfoPanel;
        [FormerlySerializedAs("factionPanel")] [SerializeField] private FactionPanel _factionPanel;
        [FormerlySerializedAs("cursorHandler")] [SerializeField] private CursorHandler _cursorHandler;
        [FormerlySerializedAs("finishScreen")] [SerializeField] private GameFinishedScreenBehaviour _finishScreen;
        [FormerlySerializedAs("hiredHandPanel")] [SerializeField] private HiredHandPanel _hiredHandPanel;

        public CursorHandler CursorHandler => _cursorHandler;
        public BuildPanel BuildPanel => _buildPanel;
        public TowerInfoPanel TowerInfoPanel => _towerInfoPanel;
        public FactionPanel FactionPanel => _factionPanel;
        public GameFinishedScreenBehaviour FinishScreen => _finishScreen;
        public HiredHandPanel HiredHandPanel => _hiredHandPanel;

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
