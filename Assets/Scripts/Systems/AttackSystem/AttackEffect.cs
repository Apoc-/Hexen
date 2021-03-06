﻿using System;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Systems.AttackSystem
{
    public abstract class AttackEffect
    {
        //todo not sure about this yet
        private float _triggerChance;

        protected AttackEffect(float triggerChance = 1.0f)
        {
            _triggerChance = triggerChance;
        }

        public void OnHit(Tower source, Npc target)
        {
            Random r = new Random();
            var n = (float)r.NextDouble();

            if (n <= _triggerChance) ApplyEffect(source, target);
        }

        protected abstract void ApplyEffect(Tower source, Npc target);
    }
}