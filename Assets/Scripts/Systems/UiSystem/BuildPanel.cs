using System.Collections.Generic;
using System.Linq;
using Systems.TowerSystem;
using UnityEngine;

namespace Systems.UiSystem
{
    class BuildPanel : MonoBehaviour
    {
        private List<TowerBuildButton> towerButtons = new List<TowerBuildButton>();
        [SerializeField] private GameObject towerButtonContainer;

        public void AddBuildButtonForTower(Tower tower)
        {
            TowerBuildButton button =
                Instantiate(Resources.Load<TowerBuildButton>("Prefabs/UI/TowerBuildButton"));

            button.Tower = tower;
            button.SetIcon(button.Tower.Icon);
            button.transform.SetParent(towerButtonContainer.transform);

            button.PriceTag.text = "" + tower.GoldCost;

            towerButtons.Add(button);
        }

        public void RemoveBuildButton(TowerBuildButton button, bool placed)
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
