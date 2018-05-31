using UnityEngine;

namespace Hexen
{
    class TowerSelectionManager : MonoBehaviour
    {
        private Tower selectedTower;
        private GameObject activeRangeIndicator;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SelectTower();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                UnselectTower();
            }
        }

        private void SelectTower()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //todo expand for npcs
            LayerMask mask = LayerMask.GetMask("Towers");

            if (Physics.Raycast(ray, out hit))
            {
                var tower = hit.transform.gameObject.GetComponent<Tower>();

                if (tower != null && tower.IsPlaced)
                {
                    selectedTower = tower;
                    DisplayRangeIndicator(tower);
                }
            }
        }

        private void UnselectTower()
        {
            selectedTower = null;
            Destroy(activeRangeIndicator);
        }

        private void DisplayRangeIndicator(Tower tower)
        {
            if (activeRangeIndicator != null)
            {
                Destroy(activeRangeIndicator);
            }

            activeRangeIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Entities/RangeIndicator"));
            activeRangeIndicator.transform.SetParent(tower.transform);
            activeRangeIndicator.transform.localPosition = Vector3.zero;

            var range = tower.HeightDependantAttackRange();
            activeRangeIndicator.transform.localScale = new Vector3(range, 1, range);
        }
    }
}
