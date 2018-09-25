using System;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class AttackEffect
    {
        //todo not sure about this yet
        private float triggerChance;

        protected AttackEffect(float triggerChance = 1.0f)
        {
            this.triggerChance = triggerChance;
        }

        public void OnHit(Tower source, Npc target)
        {
            Random r = new Random();
            var n = (float)r.NextDouble();

            if (n <= triggerChance) ApplyEffect(source, target);
        }

        protected abstract void ApplyEffect(Tower source, Npc target);
    }
}