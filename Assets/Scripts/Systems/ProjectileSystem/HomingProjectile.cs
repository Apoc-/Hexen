using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class HomingProjectile : Projectile
    {
        protected override void UpdateTransform()
        {
            if (this.Target == null)
            {
                Destroy(gameObject);
                return;
            }

            var dist = Vector3.Distance(this.Target.transform.position, this.transform.position);
            if (dist < 0.1f)
            {
                transform.position = this.Target.transform.position;
                return;
            }

            Vector3 target = Target.GetComponentInChildren<Collider>().transform.position;
            Vector3 position = this.transform.position;

            Vector3 direction = target - position;
            direction.Normalize();

            UpdateVelocity();

            transform.SetPositionAndRotation(
                position + direction * (Speed * Time.fixedDeltaTime) + Velocity,
                Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(90, 0, 0));

        }
    }
}
