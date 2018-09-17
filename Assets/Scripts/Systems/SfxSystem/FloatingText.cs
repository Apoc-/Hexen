using UnityEngine;

namespace Assets.Scripts.Systems.SfxSystem
{
    public class FloatingText : MonoBehaviour
    {
        public bool IsPlaying { get; set; }
        public float Duration { get; set; }

        private float timeRunning = 0.0f;

        private TextMesh textMesh;

        public void Awake()
        {
            IsPlaying = false;
            Duration = 3.0f;
            textMesh = GetComponent<TextMesh>();
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
            textMesh.characterSize = size;
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