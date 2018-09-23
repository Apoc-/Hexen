using TMPro;
using UnityEngine;

namespace Assets.Scripts.Systems.SfxSystem
{
    public class FloatingTextBehaviour : MonoBehaviour
    {
        public bool IsPlaying { get; set; }
        public float Duration { get; set; }

        private float timeRunning = 0.0f;

        private TextMeshPro textMesh;

        public GameObject Container;

        public void Awake()
        {
            IsPlaying = false;
            Duration = 3.0f;
            textMesh = GetComponent<TextMeshPro>();
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            if (IsPlaying) timeRunning += deltaTime;

            transform.Translate(Vector3.up * deltaTime / 2, Space.World);

            if (timeRunning >= Duration) StopPlaying();
        }

        private void StopPlaying()
        {
            IsPlaying = false;
            timeRunning = 0.0f;
        }

        public void SetSize(float size)
        {
            textMesh.fontSize = size;
        }

        public void SetColor(Color color)
        {
            textMesh.color = color;
        }

        public void SetText(string text)
        {
            textMesh.text = text;
        }
    }
}