using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.TowerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Systems.UiSystem
{
    class TowerInfoPanelBehaviour : MonoBehaviour
    {
        private string towerInfoElementPrefab = "Prefabs/UI/TowerInfoElement";

        [SerializeField]
        private GameObject towerInfoElementsContainer;

        //[SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI level;

        private List<TowerInfoElement> towerInfoElements = new List<TowerInfoElement>();
        private bool isEnabled = false;

        private Tower infoTower;

        public void Update()
        {
            if (isEnabled)
            {
                UpdateInfoPopupData();  
            }
        }

        public void EnableTowerInfoPopup(Tower tower)
        {
            infoTower = tower;
            
            isEnabled = true;
            gameObject.SetActive(true);
        }
        
        public void DisableTowerInfoPopup()
        {
            infoTower = null;
            isEnabled = false;
            gameObject.SetActive(false);

            ClearInformation();
        }

        public void UpdateInfoPopupData()
        {
            //SetIcon(infoTower.Icon);
            SetTitle(infoTower.Name);
            SetLevel(infoTower.Level.ToString());
            SetDescription(infoTower.Description);
            ClearInfoElements();

            foreach (var attribute in infoTower.Attributes)
            {
                CreateNewInfoElement(attribute.Value);
            }
        }

        public void ClearInformation()
        {
            //SetIcon(null);
            SetTitle("");
            SetLevel("");
            SetDescription("");
            ClearInfoElements();
        }

        private void ClearInfoElements()
        {
            towerInfoElements.ForEach(element =>
            {
                Destroy(element.gameObject);
            });

            towerInfoElements = new List<TowerInfoElement>();
        }

        /*private void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }*/

        private void SetTitle(String title)
        {
            this.title.text = title;
        }

        private void SetLevel(String value)
        {
            this.level.text = value;
        }

        private void SetDescription(String description)
        {
            this.description.text = description;
        }


        private void CreateNewInfoElement(Attribute attribute)
        {
            var element = Instantiate(Resources.Load<TowerInfoElement>(towerInfoElementPrefab));
            element.transform.parent = towerInfoElementsContainer.transform;
            element.InitTowerInfoElement(attribute);

            towerInfoElements.Add(element);
        }
    }
}
