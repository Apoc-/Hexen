using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
{
    class BuildPanelBehaviour : MonoBehaviour
    {
        private List<TowerBuildButtonBehaviour> towerButtons = new List<TowerBuildButtonBehaviour>();
        [SerializeField] private GameObject towerButtonContainer;

        public void AddBuildButtonForTower(Tower tower)
        {
            TowerBuildButtonBehaviour button =
                Instantiate(Resources.Load<TowerBuildButtonBehaviour>("Prefabs/UI/TowerBuildButton"));

            button.Tower = tower;
            button.SetIcon(button.Tower.Icon);
            button.transform.SetParent(towerButtonContainer.transform);

            button.PriceTag.text = "" + tower.GoldCost;

            towerButtons.Add(button);
        }

        public void RemoveBuildButton(TowerBuildButtonBehaviour button, bool placed)
        {
            if (button == null) return;

            towerButtons.Remove(button);
            
            Destroy(button.gameObject);

            if (!placed)
            {
                button.Tower.Remove();
                Destroy(button.Tower.gameObject);
            }
        }

        public void RemoveBuildButtonForTower(Tower tower)
        {
            var buildButton = towerButtons.FirstOrDefault(button => button.Tower == tower);
            RemoveBuildButton(buildButton, false);
        }
    }
}
