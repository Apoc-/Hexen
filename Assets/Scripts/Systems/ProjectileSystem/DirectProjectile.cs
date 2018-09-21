using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class DirectProjectile : Projectile
    {
        private Vector3 targetPosition = Vector3.zero;
        protected bool targetReached = false;
        private bool targetAquired = false;

        protected override void UpdateTransform()
        {
            if (targetReached) return;
            if (!targetAquired)
            {
                AquireInitialTarget();
            }

            Vector3 position = this.transform.position;

            Vector3 direction = targetPosition - position;
            direction.Normalize();

            UpdateVelocity();

            transform.SetPositionAndRotation(
                position + direction * (Speed * Time.fixedDeltaTime) + Velocity,
                Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(90, 0, 0));

            var dist = Vector3.Distance(this.transform.position, targetPosition);
            if (dist <= 0.1f)
            {
                targetReached = true;
            }
        }

        private void AquireInitialTarget()
        {
            if (this.Target == null)
            {
                Destroy(gameObject);
                return;
            }

            targetPosition = Target.GetComponentInChildren<Collider>().transform.position;
            targetAquired = true;
        }
    }
}
