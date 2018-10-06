using System.Collections;
using Systems.AttackSystem;
using Systems.FactionSystem;
using Systems.HiredHandSystem;
using Systems.MapSystem;
using Systems.SpecialEffectSystem;
using Systems.TowerSystem;
using Systems.UiSystem.Core;
using Systems.UiSystem.Popups;
using Systems.WaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems.GameSystem
{
    enum HexenScene
    {
        StartMenuScene,
        GameScene
    }

    class GameManager : Singleton<GameManager>
    {
        public bool PlayerReady;

        // ReSharper disable once NotAccessedField.Local
        private HexenScene _currentScene = HexenScene.StartMenuScene;

        private Player _player;
        public Player Player
        {
            get
            {
                if (_player == null)
                {
                    _player = InitPlayer();
                }

                return _player;
            }
        }

        private TowerBuildManager _towerBuildManager;
        public TowerBuildManager TowerBuildManager
        {
            get
            {
                if (_towerBuildManager == null)
                {
                    _towerBuildManager = FindObjectOfType<TowerBuildManager>();
                }

                return _towerBuildManager;
            }
        }

        private HiredHandMerchant _hiredHandMerchant;
        public HiredHandMerchant HiredHandMerchant
        {
            get
            {
                if (_hiredHandMerchant == null)
                {
                    _hiredHandMerchant = FindObjectOfType<HiredHandMerchant>();
                }

                return _hiredHandMerchant;
            }
        }

        private WaveSpawner _waveSpawner;
        public WaveSpawner WaveSpawner
        {
            get
            {
                if (_waveSpawner == null)
                {
                    _waveSpawner = FindObjectOfType<WaveSpawner>();
                }

                return _waveSpawner;
            }
        }

        private UIManager _uiManager;

        public UIManager UIManager
        {
            get
            {
                if (_uiManager == null)
                {
                    _uiManager = FindObjectOfType<UIManager>();
                }

                return _uiManager;
            }
        }

        private PopupManager _popupManager;

        public PopupManager PopupManager
        {
            get
            {
                if (_popupManager == null)
                {
                    _popupManager = FindObjectOfType<PopupManager>();
                }

                return _popupManager;
            }
        }

        private MapManager _mapManager;

        public MapManager MapManager
        {
            get
            {
                if (_mapManager == null)
                {
                    _mapManager = FindObjectOfType<MapManager>();
                }

                return _mapManager;
            }
        }

        private TowerSelectionManager _towerSelectionManager;

        public TowerSelectionManager TowerSelectionManager
        {
            get
            {
                if (_towerSelectionManager == null)
                {
                    _towerSelectionManager = FindObjectOfType<TowerSelectionManager>();
                }

                return _towerSelectionManager;
            }
        }

        private SpecialEffectManager _specialEffectManager;
        public SpecialEffectManager SpecialEffectManager
        {
            get
            {
                if (_specialEffectManager == null)
                {
                    _specialEffectManager = FindObjectOfType<SpecialEffectManager>();
                }

                return _specialEffectManager;
            }
        }

        private FactionManager _factionManager;
        public FactionManager FactionManager
        {
            get
            {
                if (_factionManager == null)
                {
                    _factionManager = FindObjectOfType<FactionManager>();
                }

                return _factionManager;
            }
        }

        private WaveGenerator _waveGenerator;

        public WaveGenerator WaveGenerator
        {
            get
            {
                if (_waveGenerator == null)
                {
                    _waveGenerator = FindObjectOfType<WaveGenerator>();
                }

                return _waveGenerator;
            }
        }

        private TargetingHelper _targetingHelper;

        public TargetingHelper TargetingHelper
        {
            get
            {
                if (_targetingHelper == null)
                {
                    _targetingHelper = FindObjectOfType<TargetingHelper>();
                }

                return _targetingHelper;
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
            _currentScene = scene;

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
            UIManager.InitializeUI();
        }

        public void MakePlayerReady()
        {
            TowerBuildManager.GenerateStartingBuildableTowers(Player);
            PlayerReady = true;
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
            _player = null;
            _waveSpawner = null;
            _towerBuildManager = null;
        }

        private Player InitPlayer()
        {
            var player = Instantiate(Resources.Load<Player>("Prefabs/Player"));
            player.gameObject.name = "Player_" + player.Name;
            return player;
        }
    }
}
