using Assets.Scripts.Systems.GameSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.UiSystem
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
