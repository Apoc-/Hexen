using UnityEngine;

namespace Hexen
{
    class AttributeEntitySelectionManager : MonoBehaviour
    {
        private AttributeEntity selectedEntity;
        private GameObject activeRangeIndicator;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SelectEntity();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DeselectEntity();
            }
        }

        private void SelectEntity()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //todo expand for npcs
            LayerMask mask = LayerMask.GetMask("Towers");

            if (Physics.Raycast(ray, out hit))
            {
                var entity = hit.transform.gameObject.GetComponent<AttributeEntity>();

                if (entity != null && entity.GetType() == typeof(Tower))
                {
                    var twr = (Tower) entity;

                    if (twr.IsPlaced)
                    {
                        ActivateTowerSelection(twr);
                        selectedEntity = entity;
                    }
                }
            }
        }

        private void DeselectEntity()
        {
            selectedEntity = null;
        }

        private void ActivateTowerSelection(Tower tower)
        {
            DisplayRangeIndicator(tower);
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

            var range = tower.ActualAttackRange();
            activeRangeIndicator.transform.localScale = new Vector3(range, 1, range);
        }
    }
}
