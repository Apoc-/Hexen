using UnityEngine;

namespace Systems.SpecialEffectSystem
{
    public class ParticleEffectData : SpecialEffectData
    {
        public ParticleEffectData(string effectPrefabName, GameObject origin, float duration, bool followsOrigin = false, bool diesWithOrigin = true) 
            : base(effectPrefabName, origin, duration, followsOrigin, diesWithOrigin)
        {
        }

        public ParticleEffectData(string effectPrefabName, GameObject origin, Vector3 offset, float duration, bool followsOrigin = false, bool diesWithOrigin = true) 
            : base(effectPrefabName, origin, offset, duration, followsOrigin, diesWithOrigin)
        {
        }
    }
}