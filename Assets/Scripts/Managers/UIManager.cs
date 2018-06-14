using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;
using Hexen.WaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
{
    class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject goldInfo;
        [SerializeField] private GameObject lifeInfo;
        [SerializeField] private GameObject waveInfo;
        [SerializeField] private GameObject waveTimer;
        [SerializeField] private BuildPanelBehaviour buildPanel;
        [SerializeField] private InfoPopupBehaviour infoPopup;
        
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
    }
}
