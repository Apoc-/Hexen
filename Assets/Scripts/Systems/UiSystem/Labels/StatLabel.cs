using TMPro;
using UnityEngine;

namespace Systems.UiSystem.Labels
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