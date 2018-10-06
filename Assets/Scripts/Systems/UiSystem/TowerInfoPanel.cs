using System;
using System.Collections.Generic;
using Systems.TowerSystem;
using TMPro;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Systems.UiSystem
{
    class TowerInfoPanel : MonoBehaviour
    {
        private string towerInfoElementPrefab = "Prefabs/UI/TowerInfoElement";

        [SerializeField]
        private GameObject towerInfoElementsContainer;

        //[SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI level;

        public GameObject HiredHandsContainer;

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

        public void EnableTowerInfoPanel(Tower tower)
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

            //infoTower.HiredHands.ForEach(HiredHandsContainer);
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
            level.text = value;
        }

        private void SetDescription(String description)
        {
            this.description.text = description;
        }


        private void CreateNewInfoElement(Attribute attribute)
        {
            var element = Instantiate(Resources.Load<TowerInfoElement>(towerInfoElementPrefab));
            element.transform.SetParent(towerInfoElementsContainer.transform);
            element.InitTowerInfoElement(attribute);

            towerInfoElements.Add(element);
        }
    }
}
