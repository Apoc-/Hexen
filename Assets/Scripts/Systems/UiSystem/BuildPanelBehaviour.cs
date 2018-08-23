using System.Collections.Generic;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
{
    class BuildPanelBehaviour : MonoBehaviour
    {
        private int maxTowers = 4;
        private Queue<TowerBuildButtonBehaviour> towerButtons = new Queue<TowerBuildButtonBehaviour>();

        public void AddBuildButtonForTower(Tower tower)
        {
            while (towerButtons.Count >= GameManager.Instance.Player.TowerSlots)
            {
                var destrButton = towerButtons.Dequeue();
                Destroy(destrButton.gameObject);
                Debug.Log("Too many buildable towers, destroying " + destrButton.Tower.Name + ".");
            }

            TowerBuildButtonBehaviour button =
                Instantiate(Resources.Load<TowerBuildButtonBehaviour>("Prefabs/UI/TowerBuildButton"));


            button.Tower = Instantiate(tower);

            button.Tower.Name = tower.Name;
            button.Tower.transform.parent = GameManager.Instance.TowerBuildManager.BuildableTowers.transform;

            button.Tower.gameObject.SetActive(false);

            button.gameObject.GetComponent<Image>().sprite = button.Tower.Icon;
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
