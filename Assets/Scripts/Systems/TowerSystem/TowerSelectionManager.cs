using System.Collections.Generic;
using Systems.AttributeSystem;
using Systems.GameSystem;
using Systems.MapSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.TowerSystem
{
    class TowerSelectionManager : MonoBehaviour
    {
        public Tower CurrentSelectedTower;
        private List<RangeIndicator> activeRangeIndicators = new List<RangeIndicator>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) 
                && !EventSystem.current.IsPointerOverGameObject()
                && !GameManager.Instance.TowerBuildManager.IsBuilding)
            {
                var tower = CheckForTower();
                SelectTower(tower);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DeselectTower();
            }
        }

        private Tower CheckForTower()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            LayerMask mask = LayerMask.GetMask("Tiles", "Towers");

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) return null;

            var tile = hit.transform.gameObject.GetComponent<Tile>();
            var tower = hit.transform.gameObject.GetComponentInParent<Tower>();

            if (tower != null) return tower;

            return tile.PlacedTower;
        }

        public void SelectTower(Tower tower)
        {
            if (tower == null || CurrentSelectedTower == tower) return;

            DeselectTower();
            CurrentSelectedTower = tower;
            CreateRangeIndicators(CurrentSelectedTower);

            GameManager.Instance.UIManager.TowerInfoPanel.EnableTowerInfoPanel(CurrentSelectedTower);
        }

        public void DeselectTower()
        {
            CurrentSelectedTower = null;
            DestroyRangeIndicators();
            GameManager.Instance.UIManager.TowerInfoPanel.DisableTowerInfoPopup();
        }

        private void CreateRangeIndicators(Tower tower)
        {
            if (tower.HasAttribute(AttributeName.AttackRange))
            {
                InstantiateRangeIndicator(
                    tower, 
                    tower.GetAttribute(AttributeName.AttackRange), 
                    GameSettings.AttackRangeIndicatorColor);
            }

            if (tower.HasAttribute(AttributeName.AuraRange))
            {
                var indicator = InstantiateRangeIndicator(
                    tower,
                    tower.GetAttribute(AttributeName.AuraRange),
                    GameSettings.AuraRangeIndicatorColor);

                indicator.transform.Translate(new Vector3(0,0.05f,0));
            }
        }

        private RangeIndicator InstantiateRangeIndicator(Tower tower, Attribute attribute, Color color)
        {
            var indicatorPrefab = Resources.Load<GameObject>("Sfx/RangeIndicator");
            var go = Instantiate(indicatorPrefab);
            var indicator = go.GetComponent<RangeIndicator>();

            indicator.InitRangeIndicator(attribute, color);
            indicator.transform.parent = tower.transform;
            indicator.transform.localPosition = new Vector3(0, 0.25f, 0);

            activeRangeIndicators.Add(indicator);

            return indicator;
        }

        public void DestroyRangeIndicators()
        {
            activeRangeIndicators.ForEach(indicator =>
            {
                if (indicator == null) return;
                Destroy(indicator.gameObject);
            });

            activeRangeIndicators = new List<RangeIndicator>();
        }

        
    }
}
