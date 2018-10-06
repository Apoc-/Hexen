using Systems.GameSystem;
using TMPro;
using UnityEngine;

namespace Systems.UiSystem.Core
{
    class GameFinishedScreenBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;

        public void EnableWithMessage(string msg)
        {
            title.text = msg;
            gameObject.SetActive(true);
        }

        public void HandleOkButtonClick()
        {
            GameManager.Instance.ReturnToMenu();
        }
    }
}
