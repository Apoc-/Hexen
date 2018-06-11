using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen.GameData
{
    class TowerDataManager : MonoBehaviour
    {
        private List<Tower> towers = new List<Tower>();

        public void InitializeTowers()
        {
            CreateTowerGameObject<ArrowTower>();
            CreateTowerGameObject<CannonTower>();
        }

        public void Update()
        {
            
        }

        public void CreateTowerGameObject<T>() where T : Tower
        {
            GameObject go = new GameObject();
            Tower tower = go.AddComponent<T>();

            go.name = tower.Name;
            go.transform.parent = transform;

            towers.Add(tower);
        }

        public List<Tower> GetAvailableTowers()
        {
            return towers;
        }
    }
}
