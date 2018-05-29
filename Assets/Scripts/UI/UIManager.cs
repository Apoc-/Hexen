using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
{
    class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject goldInfo;
        [SerializeField] private GameObject lifeInfo;
        [SerializeField] private GameObject waveInfo;
        [SerializeField] private BuildPanelBehaviour buildPanel;
        public BuildPanelBehaviour BuildPanel
        {
            get
            {
                return buildPanel;
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

            var currentWave = GameManager.Instance.WaveSpawner.CurrentWave;
            var totalWaves = GameManager.Instance.WaveSpawner.TotalWaves;
            waveInfo.GetComponentInChildren<Text>().text = currentWave + "/" + totalWaves;
        }
    }
}
