using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;

namespace Hexen
{
    class TowerBuildButtonBehaviour : MonoBehaviour
    {
        public Tower Tower;

        public void OnButtonClicked()
        {
            GameManager.Instance.TowerBuildManager.PickUpTower(Tower);
        }
    }
}
