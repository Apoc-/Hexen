using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class GameManager : Singleton<GameManager>
    {
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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator LoadSceneAsynch(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            InitGame();
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

        private void InitTowerBuildManager()
        {
            TowerBuildManager.LoadTowers();
        }

        private Player InitPlayer()
        {
            var player = Instantiate(Resources.Load<Player>("Prefabs/Player"));
            player.gameObject.name = "Player_" + player.Name;
            return player;
        }
    }
}
