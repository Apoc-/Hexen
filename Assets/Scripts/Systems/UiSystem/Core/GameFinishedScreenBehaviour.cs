using Systems.GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.UiSystem.Core
{
    class GameFinishedScreenBehaviour : MonoBehaviour
    {
        [FormerlySerializedAs("title")] [SerializeField] private TextMeshProUGUI _title;

        public void EnableWithMessage(string msg)
        {
            _title.text = msg;
            gameObject.SetActive(true);
        }

        public void HandleOkButtonClick()
        {
            GameManager.Instance.ReturnToMenu();
        }
    }
}
