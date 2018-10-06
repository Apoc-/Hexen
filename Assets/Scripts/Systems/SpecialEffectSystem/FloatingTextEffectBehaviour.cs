using TMPro;
using UnityEngine;

namespace Systems.SpecialEffectSystem
{
    public class FloatingTextEffectBehaviour : MonoBehaviour
    {
        private TextMeshPro _textMesh;

        public void Awake()
        {
            _textMesh = GetComponent<TextMeshPro>();
        }

        public void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime / 2, Space.World);
        }

        public void SetSize(float size)
        {
            _textMesh.fontSize = size;
        }

        public void SetColor(Color color)
        {
            _textMesh.color = color;
        }

        public void SetText(string text)
        {
            _textMesh.text = text;
        }
    }
}