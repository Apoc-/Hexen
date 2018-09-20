using UnityEngine;

namespace Assets.Scripts.Systems.SfxSystem
{
    public class TrailEffect
    {
        public GameObject Origin { get; private set; }
        public GameObject Container { get; private set; }
        public TrailRenderer Trail { get; private set; }

        public TrailEffect(GameObject origin, GameObject container, TrailRenderer trail)
        {
            Origin = origin;
            Container = container;
            Trail = trail;
        }
    }
}