using System;
using UnityEngine;

namespace Assets.Scripts.Systems.GameSystem
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private float panSpeed = 0.5f;

        [SerializeField] private float zoomMin = 2.5f;
        [SerializeField] private float zoomMax = 10.0f;
        [SerializeField] private float rotXMin = 20.0f;
        [SerializeField] private float rotXMax = 90.0f;
        [SerializeField] private float zoomSpeed = 1.5f;

        private void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var zoomInput = Input.GetAxis("Mouse ScrollWheel");
            var dead = 0.1;

            horizontalInput = (Math.Abs(horizontalInput) > dead) ? horizontalInput : 0;
            verticalInput = (Math.Abs(verticalInput) > dead) ? verticalInput : 0;

            PanCamera(horizontalInput, verticalInput);
            if (Math.Abs(zoomInput) > 0)
            {
                ZoomCamera(zoomInput);
            }
        }

        private void ZoomCamera(float zoomInput)
        {
            var cam = GetComponent<Camera>();
            var zoom = -zoomInput * zoomSpeed;

            transform.Translate(0, zoom, 0, Space.World);
            var pos = transform.position;
            var y = Mathf.Clamp(transform.position.y, zoomMin, zoomMax);
            transform.position = new Vector3(pos.x, y, pos.z);

            var zoomVal = transform.position.y;
            var p = (zoomVal - zoomMin) / (zoomMax - zoomMin);
            var rotVal = (rotXMax - rotXMin) * p + rotXMin;
            var euler = transform.localEulerAngles;
            euler.x = rotVal;

            transform.rotation = Quaternion.Euler(euler);
        }

        private void PanCamera(float horizontalInput, float verticalInput)
        {
            gameObject.transform.Translate(Vector3.right * horizontalInput * panSpeed);

            var pos = gameObject.transform.position;
            pos.z += verticalInput * panSpeed;

            //clamp movement to map extends
            var upper = GameManager.Instance.MapManager.UpperBound;
            var left = GameManager.Instance.MapManager.LeftBound;
            var height = GameManager.Instance.MapManager.MapHeight;
            var width = GameManager.Instance.MapManager.MapWidth;

            if (pos.x < left) pos.x = left;
            if (pos.x > left + width) pos.x = left + width;

            if (pos.z > upper) pos.z = upper;
            if (pos.z < upper - height) pos.z = upper - height;

            gameObject.transform.position = pos;
        }
    }
}
