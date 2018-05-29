using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using Assets.Scripts.GameLogic;
using Hexen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hexen
{
    enum HexenScene
    {
        StartMenuScene,
        GameScene
    }

    class GameManager : Singleton<GameManager>
    {
        private HexenScene currentScene = HexenScene.StartMenuScene;

        private Player player;
        public Player Player
        {
            get
            {
                if (player == null)
                {
                    player = InitPlayer();
                }

                return player;
            }
        }

        private TowerBuildManager towerBuildManager;
        public TowerBuildManager TowerBuildManager
        {
            get
            {
                if (towerBuildManager == null)
                {
                    towerBuildManager = FindObjectOfType<TowerBuildManager>();
                }

                return towerBuildManager;
            }
        }

        private WaveSpawner waveSpawner;
        public WaveSpawner WaveSpawner
        {
            get
            {
                if (waveSpawner == null)
                {
                    waveSpawner = FindObjectOfType<WaveSpawner>();
                }

                return waveSpawner;
            }
        }

        private UIManager uiManager;

        public UIManager UIManager
        {
            get
            {
                if (uiManager == null)
                {
                    uiManager = FindObjectOfType<UIManager>();
                }

                return uiManager;
            }
        }

        private MapManager mapManager;

        public MapManager MapManager
        {
            get
            {
                if (mapManager == null)
                {
                    mapManager = FindObjectOfType<MapManager>();
                }

                return mapManager;
            }
        }

        private AttributeEntitySelectionManager attributeEntitySelectionManager;

        public AttributeEntitySelectionManager AttributeEntitySelectionManager
        {
            get
            {
                if (attributeEntitySelectionManager == null)
                {
                    attributeEntitySelectionManager = FindObjectOfType<AttributeEntitySelectionManager>();
                }

                return attributeEntitySelectionManager;
            }
        }

        private IEnumerator LoadSceneAsynch(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            HandleSceneChange(SceneNameProvider.GetSceneFromName(sceneName));
        }

        private void HandleSceneChange(HexenScene scene)
        {
            currentScene = scene;

            switch (scene)
            {
                case HexenScene.StartMenuScene:
                    ResetGame();
                    break;
                case HexenScene.GameScene:
                    InitGame();
                    break;
            }
        }

        private void InitGame()
        {
            InitTowerBuildManager();
        }

        public void StartGame()
        {
            StartCoroutine(LoadSceneAsynch("GameScene"));
        }

        public void ExitGame()
        {
            Application.Quit();   
        }

        public void ReturnToMenu()
        {
            StartCoroutine(LoadSceneAsynch("StartMenuScene"));
        }

        public void LoseGame()
        {
            UIManager.FinishScreen.EnableWithMessage("You Lost!");
        }

        public void WinGame()
        {
            UIManager.FinishScreen.EnableWithMessage("You Won!");
        }

        private void ResetGame()
        {
            this.player = null;
            this.waveSpawner = null;
            this.towerBuildManager = null;
        }

        private void InitTowerBuildManager()
        {
            TowerBuildManager.LoadTowers();
            TowerBuildManager.GenerateStartingBuildableTowers(Player);
        }

        private Player InitPlayer()
        {
            var player = Instantiate(Resources.Load<Player>("Prefabs/Player"));
            player.gameObject.name = "Player_" + player.Name;
            return player;
        }
    }
}
