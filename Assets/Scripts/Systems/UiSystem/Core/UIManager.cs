﻿using Systems.HiredHandSystem;
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


        public CursorHandler CursorHandler
        {
            get
            {
                return cursorHandler;
            }
        }

        public BuildPanel BuildPanel
        {
            get
            {
                return buildPanel;
            }
        }

        public TowerInfoPanel TowerInfoPanel
        {
            get
            {
                return towerInfoPanel;
            }
        }

        public FactionPanel FactionPanel
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

        public HiredHandPanel HiredHandPanel
        {
            get
            {
                return hiredHandPanel;
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
