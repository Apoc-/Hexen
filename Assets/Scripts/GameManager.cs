using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;
using UnityEngine;

namespace Assets.Scripts
{
    class GameManager : Singleton<GameManager>
    {
        public Player Player;
        public TowerBuildManager TowerBuildManager;

        public void Start()
        {
            InitPlayer();
            TowerBuildManager.LoadTowers();
        }

        private void InitPlayer()
        {
            Player = Instantiate(Resources.Load<Player>("Prefabs/Player"));
            Player.gameObject.name = "Player_" + Player.Name;
        }
    }
}
