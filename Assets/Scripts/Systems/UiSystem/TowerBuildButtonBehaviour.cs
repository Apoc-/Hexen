using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.UiSystem
{
    class TowerBuildButtonBehaviour : MonoBehaviour
    {
        public Tower Tower;

        [SerializeField]
        private GameObject buttonActiveEffect;
        private bool activated = false;
        
        public void OnButtonClicked()
        {
            if (!activated)
            {
                GameManager.Instance.TowerBuildManager.PickUpTower(this);
                GameManager.Instance.UIManager.InfoPopup.EnableTowerInfoPopup(Tower, Input.mousePosition);
                SetButtonActive();
            }
        }

        public void SetButtonActive()
        {
            buttonActiveEffect.gameObject.SetActive(true);
            activated = true;
        }

        public void SetButtonInactive()
        {
            buttonActiveEffect.gameObject.SetActive(false);
            activated = false;
        }
    }
}
