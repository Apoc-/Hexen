using System;
using Assets.Scripts.Systems.GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.HandSystem
{
    public class HiredHandButton : MonoBehaviour
    {
        [SerializeField] private GameObject buttonActiveEffect;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI priceTag;

        private HiredHandItem item;

        private float hoverTime = 0.75f;
        private float hoverDuration = 0.0f;
        private bool isHovering = false;

        private bool isActive = false;

        public void InitButton(HiredHandItem item)
        {
            this.item = item;
            icon.sprite = item.Icon;
            priceTag.text = item.Cost.ToString();
        }

        public void Update()
        {
            if (isHovering)
            {
                hoverDuration += Time.deltaTime;
            }

            if (hoverDuration >= hoverTime)
            {
                GameManager.Instance.PopupManager.DisplayHiredHandPopup(item);
            }
        }

        public void OnPointerEnter()
        {
            isHovering = true;
            hoverDuration = 0;
        }

        public void OnPointerExit()
        {
            isHovering = false;
            hoverDuration = 0;
            GameManager.Instance.PopupManager.DestroyPopup();
        }

        public void OnButtonClicked()
        {
            if (isActive)
            {
                isActive = false;
                //GameManager.Instance.UIManager.HiredHandPanel.UnstageHiredHand(this);
                
            } else
            {
                isActive = true;
                //GameManager.Instance.UIManager.HiredHandPanel.StageHiredHand(this);
            }

            buttonActiveEffect.SetActive(isActive);
        }
    }
}