using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
{
    class BuildPanelBehaviour : MonoBehaviour
    {
        private int maxTowers = 4;
        private Queue<TowerBuildButtonBehaviour> towerButtons = new Queue<TowerBuildButtonBehaviour>();

        public void AddBuildButtonForTower(Tower tower)
        {
            while (towerButtons.Count >= GameManager.Instance.Player.MaxBuildableTowers)
            {
                var destrButton = towerButtons.Dequeue();
                Destroy(destrButton.gameObject);
                Debug.Log("Too many buildable towers, destroying " + destrButton.Tower.Name + ".");
            }

            TowerBuildButtonBehaviour button =
                Instantiate(Resources.Load<TowerBuildButtonBehaviour>("Prefabs/TowerBuildButton"));

            button.Tower = tower;
            button.gameObject.GetComponent<Image>().sprite = tower.Icon;
            button.transform.SetParent(gameObject.transform);

            towerButtons.Enqueue(button);
        }

        public void RemoveBuildButton(TowerBuildButtonBehaviour button)
        {
            var list = new List<TowerBuildButtonBehaviour>(towerButtons);
            list.Remove(button);
            towerButtons = new Queue<TowerBuildButtonBehaviour>(list);

            Destroy(button.gameObject);
        }
    }
}
