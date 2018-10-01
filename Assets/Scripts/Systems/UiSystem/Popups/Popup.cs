using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
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