using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
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
