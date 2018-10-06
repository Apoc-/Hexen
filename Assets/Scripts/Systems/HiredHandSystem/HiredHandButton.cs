using Systems.GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.HiredHandSystem
{
    public class HiredHandButton : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonActiveEffect;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _priceTag;

        private HiredHandItem _item;

        private float _hoverTime = 0.75f;
        private float _hoverDuration;
        private bool _isHovering;

        private bool _isActive;

        public void InitButton(HiredHandItem item)
        {
            _item = item;
            _icon.sprite = item.Icon;
            _priceTag.text = item.Cost.ToString();
        }

        public void Update()
        {
            if (_isHovering)
            {
                _hoverDuration += Time.deltaTime;
            }

            if (_hoverDuration >= _hoverTime)
            {
                GameManager.Instance.PopupManager.DisplayHiredHandPopup(_item);
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
            if (_isActive)
            {
                _isActive = false;
                //GameManager.Instance.UIManager.HiredHandPanel.UnstageHiredHand(this);
                
            } else
            {
                _isActive = true;
                //GameManager.Instance.UIManager.HiredHandPanel.StageHiredHand(this);
            }

            _buttonActiveEffect.SetActive(_isActive);
        }
    }
}