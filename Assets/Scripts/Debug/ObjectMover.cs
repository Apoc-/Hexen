using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private bool selected = false;

	void Update ()
	{
	    if (Input.GetMouseButton(0) && selected == false)
	    {
	        RaycastHit hit;
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, 100.0f))
	        {
	            var col = this.GetComponent<Collider>();

                if (hit.collider == this.GetComponent<Collider>())
	            {
	                col.enabled = false;
	                selected = true;
                }
            }
	    }

	    if (selected)
	    {
	        RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, 100.0f))
	        {
	            var pos = hit.point;
	            pos.z = -0.5f;
	            gameObject.transform.position = pos;
            }
	    }
	}
}
