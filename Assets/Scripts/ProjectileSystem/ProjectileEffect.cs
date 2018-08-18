using System;

namespace Hexen
{
    public abstract class ProjectileEffect
    {
        //todo not sure about this yet
        private float triggerChance;

        protected ProjectileEffect(float triggerChance = 1.0f)
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