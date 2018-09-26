using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

namespace Assets.Scripts.Systems.SfxSystem
{
    public class LightningEffectData : SpecialEffectData
    {
        private GameObject target;

        public LightningEffectData(string effectPrefabName, GameObject origin, GameObject target, float duration)
            : base(effectPrefabName, origin, Vector3.zero, duration, true, false)
        {
            this.target = target;
        }
    }
}