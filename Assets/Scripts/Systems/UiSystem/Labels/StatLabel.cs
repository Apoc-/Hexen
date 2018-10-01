using TMPro;
using UnityEngine;

namespace Assets.Scripts.Systems.UiSystem
{
    public abstract class StatLabel : MonoBehaviour
    {
        private void Update()
        {
            UpdateValue();
        }

        protected abstract string GetValue();

        private void UpdateValue()
        {
            GetComponentInChildren<TextMeshProUGUI>().text = GetValue();
        }
    }
}