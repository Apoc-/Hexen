using Systems.TowerSystem;

namespace Systems.NpcSystem
{
    public class NpcDot
    {
        private float activeTime;
        private readonly float duration;

        private float ticksPerSecond;
        private float lastTick;

        public float Damage { get; private set; }
        public Tower Source { get; private set; }

        public NpcDot(float duration, float damage, float ticksPerSecond, Tower source)
        {
            this.activeTime = 0;
            this.lastTick = 0;
            this.duration = duration;
            this.ticksPerSecond = ticksPerSecond;

            Damage = damage;
            Source = source;
        }

        public bool ShouldTick(float currentTime)
        {
            return lastTick < currentTime - (1.0f / ticksPerSecond);
        }

        public void DoTick(float currentTime)
        {
            lastTick = currentTime;
        }

        public bool IsFinished()
        {
            return activeTime > duration;
        }

        public void IncreaseActiveTime(float increment)
        {
            activeTime += increment;
        }
    }
}