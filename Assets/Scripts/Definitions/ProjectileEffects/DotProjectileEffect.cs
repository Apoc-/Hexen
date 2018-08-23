using System.Timers;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class DotProjectileEffect : ProjectileEffect, AttributeEffectSource
    {
        private float timeActive = 0;
        private float interval = 1000;

        private float damage;
        private float duration;
        
        private Timer timer;
        private Npc target;
        private Tower source;

        public DotProjectileEffect(float damage, float duration, float triggerChance = 1) : base(triggerChance)
        {
            this.damage = damage;
            this.duration = duration;
        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            this.target = target;
            this.source = source;

            timer = new Timer(interval);
            timer.Elapsed += OnElapsed;
            timer.Start();
        }

        private void OnElapsed(object src, ElapsedEventArgs e)
        {
            if (timeActive >= this.duration || target.CurrentHealth <= 0)
            {
                timer.Stop();
            }

            timeActive += this.interval;

            this.target.DealDamage(this.damage, this.source);
        }
    }
}