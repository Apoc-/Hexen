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
        public Tile Target;

        public void DealDamage(Projectile projectile)
        {
            CurrentHealth -= projectile.Source.AttackDamage;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die(true);
                GiveRewards(projectile.Source);
            }
        }

        private void GiveRewards(Tower projectileSource)
        {
            projectileSource.GiveXP(XPReward);
            projectileSource.Owner.IncreaseGold(GoldReward);
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

        private void FixedUpdate()
        {
            if (this.Target == null)
            {
                this.Target = MapManager.Instance.StartTile;
                transform.position = Target.GetComponentInChildren<Collider>().transform.position;
            }

            Vector3 target = Target.GetComponentInChildren<Collider>().transform.position;
            Vector3 position = this.transform.position;

            Vector3 direction = target - position;

            if (direction.magnitude < (MovementSpeed * Time.fixedDeltaTime * 0.8f))
            {
                Target = MapManager.Instance.GetNextTileInPath(Target);
            }

            direction.Normalize();

            transform.SetPositionAndRotation(
                position + direction * (MovementSpeed * Time.fixedDeltaTime),
                Quaternion.LookRotation(direction, Vector3.up));
        }
    }
}
