using System;
using System.Collections.Generic;
using Systems.TowerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Systems.UiSystem
{
    class TowerInfoPanel : MonoBehaviour
    {
        private string _towerInfoElementPrefab = "Prefabs/UI/TowerInfoElement";

        [FormerlySerializedAs("towerInfoElementsContainer")] [SerializeField]
        private GameObject _towerInfoElementsContainer;

        //[SerializeField] private Image icon;
        [FormerlySerializedAs("title")] [SerializeField] private TextMeshProUGUI _title;
        [FormerlySerializedAs("description")] [SerializeField] private TextMeshProUGUI _description;
        [FormerlySerializedAs("level")] [SerializeField] private TextMeshProUGUI _level;

        public GameObject HiredHandsContainer;

        private List<TowerInfoElement> _towerInfoElements = new List<TowerInfoElement>();
        private bool _isEnabled;

        private Tower _infoTower;

        public void Update()
        {
            if (_isEnabled)
            {
                UpdateInfoPopupData();  
            }
        }

        public void EnableTowerInfoPanel(Tower tower)
        {
            _infoTower = tower;
            
            _isEnabled = true;
            gameObject.SetActive(true);
        }
        
        public void DisableTowerInfoPopup()
        {
            _infoTower = null;
            _isEnabled = false;
            gameObject.SetActive(false);

            ClearInformation();
        }

        public void UpdateInfoPopupData()
        {
            //SetIcon(infoTower.Icon);
            SetTitle(_infoTower.Name);
            SetLevel(_infoTower.Level.ToString());
            SetDescription(_infoTower.Description);
            ClearInfoElements();

            foreach (var attribute in _infoTower.Attributes)
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
            _towerInfoElements.ForEach(element =>
            {
                Destroy(element.gameObject);
            });

            _towerInfoElements = new List<TowerInfoElement>();
        }

        /*private void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }*/

        private void SetTitle(String title)
        {
            _title.text = title;
        }

        private void SetLevel(String value)
        {
            _level.text = value;
        }

        private void SetDescription(String description)
        {
            _description.text = description;
        }


        private void CreateNewInfoElement(Attribute attribute)
        {
            var element = Instantiate(Resources.Load<TowerInfoElement>(_towerInfoElementPrefab));
            element.transform.SetParent(_towerInfoElementsContainer.transform);
            element.InitTowerInfoElement(attribute);

            _towerInfoElements.Add(element);
        }
    }
}
