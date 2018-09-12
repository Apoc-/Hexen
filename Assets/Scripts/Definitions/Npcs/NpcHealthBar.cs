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
        var width = this.GetComponent<RectTransform>().rect.width;
        var healthWidth = width * percent;

        var rect = health.gameObject.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(width - healthWidth, 0);
    }
}
