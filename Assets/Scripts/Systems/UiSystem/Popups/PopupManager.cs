using Systems.HiredHandSystem;
using Systems.TowerSystem;
using TMPro;
using UnityEngine;

namespace Systems.UiSystem.Popups
{
    public class PopupManager : MonoBehaviour
    {
        private Popup _currentDisplayedPopup;
        private GameObject _curentDisplayOrigin;

        private void InitPopup(GameObject origin)
        {
            _currentDisplayedPopup = Instantiate(Resources.Load<Popup>("Prefabs/Ui/Popup"));

            _currentDisplayedPopup.transform.SetParent(transform);
            _currentDisplayedPopup.transform.position = Input.mousePosition;
            _curentDisplayOrigin = origin;
        }

        public void DisplayHiredHandPopup(HiredHandItem handItem)
        {
            if (_curentDisplayOrigin == handItem.gameObject) return;

            InitPopup(handItem.gameObject);

            string text = handItem.Name;

            _currentDisplayedPopup.SetText(text);
        }

        public void DisplayTowerPopup(Tower tower)
        {
            if (_curentDisplayOrigin == tower.gameObject) return;

            InitPopup(tower.gameObject);

            string text = tower.Name + "\n";

            var tmp = _currentDisplayedPopup.GetComponentInChildren<TextMeshProUGUI>();
            var rect = _currentDisplayedPopup.GetComponent<RectTransform>();

            var lineHeight = tmp.fontSize + tmp.lineSpacing + 2;

            var size = rect.sizeDelta;
            size.y = size.y + lineHeight*(tower.Attributes.Count() + 1);
            rect.sizeDelta = size;

            foreach (var keyValuePair in tower.Attributes)
            {
                var towerName = keyValuePair.Key;
                var towerAttribute = keyValuePair.Value;

                text += "\n" + towerName;
                text += " " + towerAttribute.Value;
            }

            //rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            _currentDisplayedPopup.SetText(text);
        }

        public void DestroyPopup()
        {
            if (_currentDisplayedPopup == null) return;

            Destroy(_currentDisplayedPopup.gameObject);
            _curentDisplayOrigin = null;
        }
    }
}