using System.Collections.Generic;
using System.Linq;
using Systems.TowerSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.UiSystem
{
    class BuildPanel : MonoBehaviour
    {
        private readonly List<TowerBuildButton> _towerButtons = new List<TowerBuildButton>();
        [FormerlySerializedAs("towerButtonContainer")] [SerializeField] private GameObject _towerButtonContainer;

        public void AddBuildButtonForTower(Tower tower)
        {
            TowerBuildButton button =
                Instantiate(Resources.Load<TowerBuildButton>("Prefabs/UI/TowerBuildButton"));

            button.Tower = tower;
            button.SetIcon(button.Tower.Icon);
            button.transform.SetParent(_towerButtonContainer.transform);

            button.PriceTag.text = "" + tower.GoldCost;

            _towerButtons.Add(button);
        }

        public void RemoveBuildButton(TowerBuildButton button, bool placed)
        {
            if (button == null) return;

            _towerButtons.Remove(button);
            
            Destroy(button.gameObject);

            if (!placed)
            {
                button.Tower.Remove();
                Destroy(button.Tower.gameObject);
            }
        }

        public void RemoveBuildButtonForTower(Tower tower)
        {
            var buildButton = _towerButtons.FirstOrDefault(button => button.Tower == tower);
            RemoveBuildButton(buildButton, false);
        }
    }
}
