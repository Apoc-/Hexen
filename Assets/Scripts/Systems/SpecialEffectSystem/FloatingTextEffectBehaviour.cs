using TMPro;
using UnityEngine;

namespace Systems.SpecialEffectSystem
{
    public class FloatingTextEffectBehaviour : MonoBehaviour
    {
        private TextMeshPro textMesh;
        public TextEffectData EffectData { get; set; }

        public void Awake()
        {
            textMesh = GetComponent<TextMeshPro>();
        }

        public void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime / 2, Space.World);
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