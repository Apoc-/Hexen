﻿using UnityEngine;
using UnityEngine.UI;

namespace Systems.NpcSystem
{
    public class NpcHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image health;

        public void Update()
        {
            //transform.LookAt(Vector3.up);
        }

        public void UpdateHealth(float percent)
        {
            if (health == null) return;

            var rect = health.gameObject.GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(percent, 1.0f);
        }
    }
}
