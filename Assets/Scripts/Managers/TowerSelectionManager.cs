using Hexen.GameData.Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityStandardAssets.CrossPlatformInput;

namespace Hexen
{
    class TowerSelectionManager : MonoBehaviour
    {
        public Tower CurrentSelectedTower;
        private GameObject activeRangeIndicator;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
            {
                SelectTower();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                UnselectTower();
            }

            if (CurrentSelectedTower != null && activeRangeIndicator != null)
            {
                if (activeRangeIndicator != null)
                {
                    RecalculateRangeIndicatorRadius();
                }
            }
        }

        

        private void SelectTower()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            LayerMask mask = LayerMask.GetMask("Towers");

            if (Physics.Raycast(ray, out hit))
            {
                var tower = hit.transform.gameObject.GetComponentInParent<Tower>();
                
                if (tower != null)
                {
                    CurrentSelectedTower = tower;
                    DisplayRangeIndicator(CurrentSelectedTower);

                    if (CurrentSelectedTower.IsPlaced)
                    {
                        GameManager.Instance.UIManager.InfoPopup.EnableTowerInfoPopup(CurrentSelectedTower, Input.mousePosition);
                    }
                }
            }
        }

        private void UnselectTower()
        {
            CurrentSelectedTower = null;
            DestroyRangeIndicator();
            GameManager.Instance.UIManager.InfoPopup.DisableTowerInfoPopup();
        }

        public void DisplayRangeIndicator(Tower tower)
        {
            if (activeRangeIndicator != null)
            {
                Destroy(activeRangeIndicator);
            }

            activeRangeIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Entities/RangeIndicator"));
            activeRangeIndicator.transform.SetParent(tower.transform);
            activeRangeIndicator.transform.localPosition = Vector3.zero;

            var range = tower.GetAttribute(AttributeName.AttackRange).Value;
            var particles = activeRangeIndicator.GetComponent<ParticleSystem>();
            var shape = particles.shape;
            shape.radius = range;
        }

        public void RecalculateRangeIndicatorRadius()
        {
            var range = CurrentSelectedTower.GetAttribute(AttributeName.AttackRange).Value;
            

            var particles = activeRangeIndicator.GetComponent<ParticleSystem>();
            var shape = particles.shape;
            var radius = shape.radius;

            if (Mathf.Abs(radius - range) > 0.0001f)
            {
                shape.radius = range;
            }
        }

        public void DestroyRangeIndicator()
        {
            if (activeRangeIndicator != null)
            {
                Destroy(activeRangeIndicator);
            }
        }
    }
}
