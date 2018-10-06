using Systems.GameSystem;
using UnityEngine;

namespace Systems.UiSystem.Core
{
    class StartGameMenuBehaviour : MonoBehaviour
    {
        public void OnStartGameClicked()
        {
            GameManager.Instance.StartGame();
        }

        public void OnExitGameClicked()
        {
            GameManager.Instance.ExitGame();
        }
    }
}
