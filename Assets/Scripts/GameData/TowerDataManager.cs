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
            RegisterTower<ArrowTower>();
            RegisterTower<CannonTower>();
            RegisterTower<HasteAuraTower>();
        }

        public void RegisterTower<T>() where T : Tower
        {
            GameObject go = new GameObject();
            Tower tower = go.AddComponent<T>();

            go.name = tower.Name;
            go.transform.parent = transform;
            go.SetActive(false);

            GameManager.Instance.FactionManager.RegisterTower(tower);
        }

        public List<Tower> GetAvailableTowers()
        {
            return towers.Where(tower => tower.IsAvailable).ToList();
        }
    }
}
