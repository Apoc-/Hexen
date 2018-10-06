using UnityEngine;

namespace Systems.SpecialEffectSystem
{
    public class TextEffectData : SpecialEffectData
    {
        public string Text { get; }
        public float Size { get; }
        public Color Color { get; }

        public TextEffectData(string text, float size, Color color, GameObject origin, float duration) 
            : base("BasicFloatingTextEffect", origin, duration, false, false)
        {
            this.Text = text;
            this.Size = size;
            this.Color = color;
        }

        public TextEffectData(string text, float size, Color color, GameObject origin, Vector3 offset, float duration)
            : base("BasicFloatingTextEffect", origin, offset, duration, false, false)
        {
            this.Text = text;
            this.Size = size;
            this.Color = color;
        }
    }
}