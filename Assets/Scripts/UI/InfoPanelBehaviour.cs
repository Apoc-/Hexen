using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
{
    class InfoPanelBehaviour : MonoBehaviour
    {
        private string infoElementPrefab = "Prefabs/UI/InfoElement";

        [SerializeField]
        private GameObject infoElementsContainer;

        [SerializeField] private Image icon;
        [SerializeField] private Text title;
        [SerializeField] private Text description;
        

        private List<GameObject> infoElements = new List<GameObject>();

        public void DisplayTowerInformation(Tower tower)
        {
            SetIcon(tower.Icon);
            SetTitle(tower.EntityName);
            SetDescription(tower.Description);

            SetInfoElements(tower.Attributes.Select(attr => attr.AttributeName + " " + attr.Value).ToList());
        }

        public void ClearInformation()
        {
            SetIcon(null);
            SetTitle("");
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
