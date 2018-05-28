using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
{
    class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject goldPanel;

        public void Update()
        {
            UpdateGoldPanel();
        }

        private void UpdateGoldPanel()
        {
            goldPanel.GetComponentInChildren<Text>().text = "" + GameManager.Instance.Player.Gold;
        }
    }
}
