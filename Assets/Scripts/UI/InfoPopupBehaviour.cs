using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Hexen
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

        private bool animating = false;
        private float animationTime = 0;
        private Tower infoTower;

        public void Update()
        {
            if (animating)
            {
                animationTime += Time.deltaTime;
                animationTime = 1f + ((animationTime -= 1f) * animationTime * animationTime);

                gameObject.transform.localScale = Vector3.Lerp(new Vector3(1.0f, 0.0f, 1.0f), Vector3.one, animationTime);
            }

            if (gameObject.transform.localScale.y >= 1.0)
            {
                ResetAnimation();
            }

            if (isEnabled)
            {
                UpdateInfoPopupData();
                
            }

            if (infoTower.IsPlaced)
            {
                UpdateInfoPopupPosition();
            }
        }

        private void ResetAnimation()
        {
            gameObject.transform.localScale = Vector3.one;
            animating = false;
            animationTime = 0;
        }

        public void EnableTowerInfoPopup(Tower tower, Vector3 screenPosition)
        {
            infoTower = tower;

            var rect = GetComponent<RectTransform>().rect;
            if (screenPosition.x + rect.width > Screen.width)
            {
                //show left of pos
                gameObject.transform.position = new Vector3(screenPosition.x - rect.width / 2 - 16, screenPosition.y);
            }
            else
            {
                //show right of pos
                gameObject.transform.position = new Vector3(screenPosition.x + rect.width / 2 + 16, screenPosition.y);
            }

            ResetAnimation();
            isEnabled = true;
            gameObject.SetActive(true);

            animating = true;
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
                .Select(attribute =>
                {
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
