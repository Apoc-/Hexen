using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        var rect = health.gameObject.GetComponent<RectTransform>();
        rect.anchorMax = new Vector2(percent, 1.0f);
    }
}
