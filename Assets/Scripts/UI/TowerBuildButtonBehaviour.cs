using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
{
    class TowerBuildButtonBehaviour : MonoBehaviour
    {
        public Tower Tower;
        public GameObject ButtonActiveEffect;
        
        public void OnButtonClicked()
        {
            GameManager.Instance.TowerBuildManager.PickUpTower(Tower, this);
            SetButtonActive();
        }

        public void SetButtonActive()
        {
            ButtonActiveEffect.gameObject.SetActive(true);
        }

        public void SetButtonInactive()
        {
            ButtonActiveEffect.gameObject.SetActive(false);
        }
    }
}
