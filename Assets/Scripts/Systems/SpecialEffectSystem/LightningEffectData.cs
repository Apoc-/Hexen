using UnityEngine;

namespace Systems.SpecialEffectSystem
{
    public class LightningEffectData : SpecialEffectData
    {
        public Vector3 Start { get; }
        public Vector3 End { get; }

        public GameObject Target { get; set; }

        public LightningEffectData(string effectPrefabName, Vector3 start, Vector3 end, float duration)
            : base(effectPrefabName, null, Vector3.zero, duration, false, false)
        {
            this.Start = start;
            this.End = end;
            
        }
        public LightningEffectData(string effectPrefabName, GameObject origin, GameObject target, float duration)
            : base(effectPrefabName, origin, Vector3.zero, duration, true, true)
        {
            this.Target = target;
        }
    }
}