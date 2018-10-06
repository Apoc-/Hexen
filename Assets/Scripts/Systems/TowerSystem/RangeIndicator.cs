using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Systems.TowerSystem
{
    public class RangeIndicator : MonoBehaviour
    {
        public ParticleSystem Outline;
        public ParticleSystem InnerEffect;
        private Attribute _attribute;

        public void Awake()
        {
            var innerEffectMain = InnerEffect.main;
            innerEffectMain.startSizeMultiplier = 0;
            
            var outlineShape = Outline.shape;
            outlineShape.radius = 0;
        }

        public void InitRangeIndicator(Attribute attribute, Color color)
        {
            _attribute = attribute;

            var innerEffectMain = InnerEffect.main;
            innerEffectMain.startColor = color;
            
            var outlineMain = Outline.main;
            outlineMain.startColor = color;
        }

        public void Update()
        {
            UpdateRangeIndicator();
        }

        private void UpdateRangeIndicator()
        {
            var range = _attribute.Value;

            UpdateOutlineRange(range);
            UpdateInnerRange(range);
        }

        private void UpdateInnerRange(float range)
        {
            var innerMain = InnerEffect.main;
            innerMain.startSizeMultiplier = range * 2 / 10;
        }

        private void UpdateOutlineRange(float range)
        {
            var baseNumberOfParticles = 600;

            var outlineMain = Outline.main;
            outlineMain.maxParticles = (int) (baseNumberOfParticles * range);

            var outlineEmission = Outline.emission;
            outlineEmission.rateOverTime = (baseNumberOfParticles*range) / 2;

            var outlineShape = Outline.shape;
            outlineShape.radius = range;
        }
    }
}