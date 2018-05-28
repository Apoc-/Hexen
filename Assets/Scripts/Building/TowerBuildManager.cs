using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using Hexen;
using UnityEngine;
using Random = System.Random;

namespace Hexen
{
    class TowerBuildManager : MonoBehaviour
    {
        private List<Tower> availableTowers;

        public void LoadTowers()
        {
            availableTowers = new List<Tower>(Resources.LoadAll<Tower>("Prefabs/Entities/Towers"));
            Debug.Log("Loaded " + availableTowers.Count + " Towers.");
        }

        public Tower GetRandomTower()
        {
            Random rnd = new Random();
            int r = rnd.Next(availableTowers.Count);

            return availableTowers[r];
        }

        public void AddRandomBuildableTower()
        {
            var t = GetRandomTower();

            GameManager.Instance.Player.AddBuildableTower(t);
        }
    }
}
