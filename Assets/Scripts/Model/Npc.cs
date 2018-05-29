using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using UnityScript.Steps;

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
        public Tile CurrentTile;

        public void DealDamage(Projectile projectile, float factor = 1.0f)
        {
            CurrentHealth = (int)(CurrentHealth - projectile.Source.AttackDamage * factor);

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
                this.Target = GameManager.Instance.MapManager.StartTile;
                transform.position = Target.GetTopCenter();
            }

            Vector3 target = Target.GetTopCenter();
            Vector3 position = this.transform.position;

            Vector3 direction = target - position;

            if (direction.magnitude < (MovementSpeed * Time.fixedDeltaTime * 0.8f))
            {
                EnterTile(Target);
                Target = GameManager.Instance.MapManager.GetNextTileInPath(Target);
            }

            direction.Normalize();

            if (direction.magnitude > 0.0001f)
            {
                transform.SetPositionAndRotation(
                    position + direction * (MovementSpeed * Time.fixedDeltaTime),
                    Quaternion.LookRotation(direction, Vector3.up));
            }
        }

        private void EnterTile(Tile tile)
        {
            if (tile != CurrentTile)
            {
                CurrentTile = tile;
                CheckEndTile(tile);
            }
        }

        private void CheckEndTile(Tile tile)
        {
            if (tile == GameManager.Instance.MapManager.EndTile)
            {
                GameManager.Instance.Player.Lives -= 1;
            }
        }
    }
}
