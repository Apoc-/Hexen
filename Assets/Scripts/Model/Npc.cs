using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hexen
{
    public class Npc : Entity
    {
        public int MaxHealth;
        public int CurrentHealth;
        public float MovementSpeed;
        public int GoldReward;
        public int XPReward;

        public void DealDamage(int damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die(true);
            }
        }

        private void Die(bool forcefully)
        {
            if (forcefully)
            {
                Explode();
            }
            else
            {
                Destroy(this);
            }
        }

        void Explode()
        {
            var exp = GetComponent<ParticleSystem>();
            exp.Play();
            Destroy(gameObject, exp.main.duration);
        }
    }
}
