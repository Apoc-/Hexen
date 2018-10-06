using UnityEngine;

namespace Systems.SpecialEffectSystem
{
    public abstract class SpecialEffectData
    {
        public string EffectPrefabName { get; private set; }
        public GameObject Origin { get; set; }
        public bool FollowsOrigin { get; private set; }
        public bool DiesWithOrigin { get; private set; }
        public float Duration { get; private set; }

        public Vector3 Offset { get; private set; }

        public float TimeActive { get; set; }
        public GameObject EffectContainer { get; set; }

        protected SpecialEffectData(string effectPrefabName, GameObject origin, float duration, bool followsOrigin = false, bool diesWithOrigin = true)
        {
            EffectPrefabName = effectPrefabName;
            Origin = origin;
            FollowsOrigin = followsOrigin;
            DiesWithOrigin = diesWithOrigin;
            Duration = duration;
            TimeActive = 0.0f;
        }

        protected SpecialEffectData(
            string effectPrefabName, 
            GameObject origin, 
            Vector3 offset, 
            float duration, 
            bool followsOrigin = false, 
            bool diesWithOrigin = true) : this(effectPrefabName, origin, duration, followsOrigin, diesWithOrigin)
        {
            Offset = offset;
        }
    }
}