using System.Collections;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.SfxSystem;
using Assets.Scripts.Systems.TowerSystem;
using Assets.Scripts.Systems.UiSystem;
using Assets.Scripts.Systems.WaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Systems.GameSystem
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

        private TowerSelectionManager towerSelectionManager;

        public TowerSelectionManager TowerSelectionManager
        {
            get
            {
                if (towerSelectionManager == null)
                {
                    towerSelectionManager = FindObjectOfType<TowerSelectionManager>();
                }

                return towerSelectionManager;
            }
        }

        private SFXManager sfxManager;
        public SFXManager SfxManager
        {
            get
            {
                if (sfxManager == null)
                {
                    sfxManager = FindObjectOfType<SFXManager>();
                }

                return sfxManager;
            }
        }

        private FactionManager factionManager;
        public FactionManager FactionManager
        {
            get
            {
                if (factionManager == null)
                {
                    factionManager = FindObjectOfType<FactionManager>();
                }

                return factionManager;
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
            FactionManager.Initialize();
            TowerBuildManager.GenerateStartingBuildableTowers(Player);
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

        

        private Player InitPlayer()
        {
            var player = Instantiate(Resources.Load<Player>("Prefabs/Player"));
            player.gameObject.name = "Player_" + player.Name;
            return player;
        }
    }
}
