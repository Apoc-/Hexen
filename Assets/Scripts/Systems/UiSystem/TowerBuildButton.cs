using Systems.GameSystem;
using Systems.TowerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UiSystem
{
    class TowerBuildButton : MonoBehaviour
    {
        public Tower Tower;

        [SerializeField]
        private GameObject buttonActiveEffect;

        [SerializeField]
        private Image icon;

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
                GameManager.Instance.PopupManager.DisplayTowerPopup(Tower);
            }
        }

        public void OnPointerEnter()
        {
            isHovering = true;
            hoverDuration = 0;
        }

        public void OnPointerExit()
        {
            isHovering = false;
            hoverDuration = 0;
            GameManager.Instance.PopupManager.DestroyPopup();
        }

        public void OnButtonClicked()
        {
            if (!activated)
            {
                GameManager.Instance.TowerBuildManager.StartBuilding(this);
                GameManager.Instance.UIManager.TowerInfoPanel.EnableTowerInfoPanel(Tower);
                SetButtonActive();
            }
        }

        public void SetIcon(Sprite sprite)
        {
            icon.GetComponent<Image>().sprite = sprite;
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
