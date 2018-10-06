using Systems.TowerSystem;

namespace Systems.NpcSystem
{
    public class NpcDot
    {
        private float _activeTime;
        private readonly float _duration;

        private readonly float _ticksPerSecond;
        private float _lastTick;

        public float Damage { get; }
        public Tower Source { get; }

        public NpcDot(float duration, float damage, float ticksPerSecond, Tower source)
        {
            _activeTime = 0;
            _lastTick = 0;
            _duration = duration;
            _ticksPerSecond = ticksPerSecond;

            Damage = damage;
            Source = source;
        }

        public bool ShouldTick(float currentTime)
        {
            return _lastTick < currentTime - (1.0f / _ticksPerSecond);
        }

        public void DoTick(float currentTime)
        {
            _lastTick = currentTime;
        }

        public bool IsFinished()
        {
            return _activeTime > _duration;
        }

        public void IncreaseActiveTime(float increment)
        {
            _activeTime += increment;
        }
    }
}