using Systems.GameSystem;
using Systems.TowerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Systems.UiSystem
{
    class TowerBuildButton : MonoBehaviour
    {
        public Tower Tower;

        [FormerlySerializedAs("buttonActiveEffect")] [SerializeField]
        private GameObject _buttonActiveEffect;

        [FormerlySerializedAs("icon")] [SerializeField]
        private Image _icon;

        private bool _activated;

        public TextMeshProUGUI PriceTag;
        private float _hoverTime = 0.75f;
        private float _hoverDuration;
        private bool _isHovering;

        public void Update()
        {
            if (_isHovering)
            {
                _hoverDuration += Time.deltaTime;
            }

            if (_hoverDuration >= _hoverTime)
            {
                GameManager.Instance.PopupManager.DisplayTowerPopup(Tower);
            }
        }

        public void OnPointerEnter()
        {
            _isHovering = true;
            _hoverDuration = 0;
        }

        public void OnPointerExit()
        {
            _isHovering = false;
            _hoverDuration = 0;
            GameManager.Instance.PopupManager.DestroyPopup();
        }

        public void OnButtonClicked()
        {
            if (!_activated)
            {
                GameManager.Instance.TowerBuildManager.StartBuilding(this);
                GameManager.Instance.UIManager.TowerInfoPanel.EnableTowerInfoPanel(Tower);
                SetButtonActive();
            }
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.GetComponent<Image>().sprite = sprite;
        }

        public void SetButtonActive()
        {
            _buttonActiveEffect.gameObject.SetActive(true);
            _activated = true;
        }

        public void SetButtonInactive()
        {
            _buttonActiveEffect.gameObject.SetActive(false);
            _activated = false;
        }
    }
}
