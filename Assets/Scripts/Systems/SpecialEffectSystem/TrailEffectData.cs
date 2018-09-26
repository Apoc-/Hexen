using UnityEngine;

namespace Assets.Scripts.Systems.SfxSystem
{
    public class TrailEffectData : SpecialEffectData
    {
        public TrailEffectData(string effectPrefabName, GameObject origin, float duration) 
            : base(effectPrefabName, origin, duration, true, false)
        {
        }
        public TrailEffectData(string effectPrefabName, GameObject origin, Vector3 offset, float duration)
            : base(effectPrefabName, origin, offset, duration, true, false)
        {
        }
    }
}