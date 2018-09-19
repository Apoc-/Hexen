using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.UiSystem
{
    class InfoPopupBehaviour : MonoBehaviour
    {
        private string infoElementPrefab = "Prefabs/UI/InfoElement";

        [SerializeField]
        private GameObject infoElementsContainer;

        [SerializeField] private Image icon;
        [SerializeField] private Text title;
        [SerializeField] private Text description;
        [SerializeField] private Text value;

        private List<GameObject> infoElements = new List<GameObject>();
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

        private void UpdateInfoPopupPosition()
        {
            if (infoTower == null)
            {
                DisableTowerInfoPopup();
                return;
            }

            var rect = GetComponent<RectTransform>().rect;
            
            var pos = Camera.main.WorldToScreenPoint(infoTower.transform.position);
            gameObject.transform.position = new Vector3(pos.x + rect.width / 2 + 16, pos.y);
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
            SetIcon(infoTower.Icon);
            SetTitle(infoTower.Name);
            SetValue(infoTower.Level.ToString());
            SetDescription(infoTower.Description);


            List<String> info = infoTower.Attributes
                .Select(kvpair => 
                {
                    var attribute = kvpair.Value;
                    var str = attribute.AttributeName + " " + attribute.Value.ToString("F2");
                    var lvlInc = attribute.LevelIncrement * 100;
                    var typeStr = (attribute.LevelIncrementType == LevelIncrementType.Percentage) ? "% / Level)" : ")";

                    if (lvlInc > 0.0f)
                    {
                        str += " (+" + lvlInc + typeStr;
                    }
                    return str;
                }).ToList();

            SetInfoElements(info);
        }

        public void ClearInformation()
        {
            SetIcon(null);
            SetTitle("");
            SetValue("");
            SetDescription("");
            SetInfoElements(new List<string>());
        }

        private void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }

        private void SetTitle(String title)
        {
            this.title.GetComponent<Text>().text = title;
        }

        private void SetValue(String value)
        {
            this.value.GetComponent<Text>().text = value;
        }

        private void SetDescription(String description)
        {
            this.description.GetComponent<Text>().text = description;
        }

        private void SetInfoElements(List<String> infoList)
        {
            while (infoElements.Count < infoList.Count)
            {
                CreateNewInfoElement();
            }

            infoElements.ForEach(element => element.transform.localScale = Vector3.one);
            var textElements = infoElements.Select(element => element.GetComponent<Text>()).ToList();
            
            textElements.ForEach(element => element.text = "");

            for (int i = 0; i < infoList.Count; i++)
            {
                textElements[i].text = infoList[i];
            }
        }

        private void CreateNewInfoElement()
        {
            var element = Instantiate(Resources.Load<GameObject>(infoElementPrefab));
            element.transform.SetParent(infoElementsContainer.transform);

            infoElements.Add(element);
        }
    }
}
