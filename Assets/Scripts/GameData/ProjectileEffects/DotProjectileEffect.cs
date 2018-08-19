using System.Runtime.CompilerServices;
using System.Timers;
using Assets.Scripts.AttributeSystem;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen
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