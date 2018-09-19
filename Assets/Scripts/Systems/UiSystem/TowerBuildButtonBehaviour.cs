using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Systems.UiSystem
{
    class TowerBuildButtonBehaviour : MonoBehaviour
    {
        public Tower Tower;

        [SerializeField]
        private GameObject buttonActiveEffect;
        private bool activated = false;

        public TextMeshProUGUI PriceTag;
        private float hoverTime = 0.75f;
        private float hoverDuration = 0.0f;
        private bool isHovering = false;

        public void Update()
        {
            if (isHovering)
            {
                hoverDuration += Time.deltaTime;
            }

            if (hoverDuration >= hoverTime)
            {
                GameManager.Instance.UIManager.InfoPopup.EnableTowerInfoPopup(Tower);
                isHovering = false;
                hoverDuration = 0;
            }
        }

        public void OnPointerEnter()
        {
            isHovering = true;
            hoverDuration = 0;
        }

        public void OnPointerExit()
        {
            if (!activated)
            {
                isHovering = false;
                hoverDuration = 0;
                GameManager.Instance.UIManager.InfoPopup.DisableTowerInfoPopup();
            }
        }

        public void OnButtonClicked()
        {
            if (!activated)
            {
                GameManager.Instance.TowerBuildManager.PickUpTower(this);
                GameManager.Instance.UIManager.InfoPopup.EnableTowerInfoPopup(Tower);
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
