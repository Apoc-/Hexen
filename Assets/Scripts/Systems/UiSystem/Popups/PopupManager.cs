using Systems.HiredHandSystem;
using Systems.TowerSystem;
using TMPro;
using UnityEngine;

namespace Systems.UiSystem.Popups
{
    public class PopupManager : MonoBehaviour
    {
        private Popup currentDisplayedPopup;
        private GameObject curentDisplayOrigin;

        private void InitPopup(GameObject origin)
        {
            currentDisplayedPopup = Instantiate(Resources.Load<Popup>("Prefabs/Ui/Popup"));

            currentDisplayedPopup.transform.SetParent(transform);
            currentDisplayedPopup.transform.position = Input.mousePosition;
            curentDisplayOrigin = origin;
        }

        public void DisplayHiredHandPopup(HiredHandItem handItem)
        {
            if (curentDisplayOrigin == handItem.gameObject) return;

            InitPopup(handItem.gameObject);

            string text = handItem.Name;

            currentDisplayedPopup.SetText(text);
        }

        public void DisplayTowerPopup(Tower tower)
        {
            if (curentDisplayOrigin == tower.gameObject) return;

            InitPopup(tower.gameObject);

            string text = tower.Name + "\n";

            var tmp = currentDisplayedPopup.GetComponentInChildren<TextMeshProUGUI>();
            var rect = currentDisplayedPopup.GetComponent<RectTransform>();

            var lineHeight = tmp.fontSize + tmp.lineSpacing + 2;

            var size = rect.sizeDelta;
            size.y = size.y + lineHeight*(tower.Attributes.Count() + 1);
            rect.sizeDelta = size;

            foreach (var keyValuePair in tower.Attributes)
            {
                var name = keyValuePair.Key;
                var attr = keyValuePair.Value;

                text += "\n" + name;
                text += " " + attr.Value;
            }

            //rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            currentDisplayedPopup.SetText(text);
        }

        public void DestroyPopup()
        {
            if (currentDisplayedPopup == null) return;

            Destroy(currentDisplayedPopup.gameObject);
            curentDisplayOrigin = null;
        }
    }
}