using Assets.Scripts.Systems.GameSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
{
    class GameFinishedScreenBehaviour : MonoBehaviour
    {
        [SerializeField] private Text title;

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
