using TMPro;
using UnityEngine;

namespace Systems.UiSystem.Popups
{
    public class Popup : MonoBehaviour
    {
        public void SetText(string text)
        {
            var tmp = GetComponentInChildren<TextMeshProUGUI>();

            tmp.text = text;
        }
    }
}