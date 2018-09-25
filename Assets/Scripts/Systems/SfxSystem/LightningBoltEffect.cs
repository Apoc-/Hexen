using System.Collections.Generic;
using DigitalRuby.LightningBolt;

namespace Assets.Scripts.Systems.SfxSystem
{

    //todo refactor
    public class LightningBoltEffect
    {
        private List<LightningBoltBehaviour> runningBolts;
        private LightningBoltBehaviour boltBehaviour;
        private float duration;

        public LightningBoltEffect(LightningBoltBehaviour boltBehaviour, float duration)
        {
            this.boltBehaviour = boltBehaviour;
            this.duration = duration;
        }

        static void Play(float duration)
        {

        }
    }
}