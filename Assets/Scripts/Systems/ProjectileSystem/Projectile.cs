using System.Collections.Generic;
using Hexen;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public abstract class Projectile : MonoBehaviour
    {
        protected List<ProjectileEffect> ProjectileEffects;

        public Tower Source;
        public Npc Target;

        protected float Speed;
        protected GameObject Model;

        private void Awake()
        {
            this.InitProjectile();
            this.InitProjectileData();
        }

        protected abstract void InitProjectileData();

        protected abstract void InitProjectile();

        private void OnCollisionEnter(Collision collision)
        {
            Collide(collision);
        }

        protected virtual void Collide(Collision collision)
        {
            var target = collision.gameObject.GetComponentInParent<Npc>();

            if (target != null)
            {
                ProjectileEffects.ForEach(effect => effect.OnHit(Source, target));

                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (this.Target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 target = Target.GetComponentInChildren<Collider>().transform.position;
            Vector3 position = this.transform.position;

            Vector3 direction = target - position;
            direction.Normalize();

            transform.SetPositionAndRotation(
                position + direction * (Speed * Time.fixedDeltaTime),
                Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(90,0,0));
        }

        protected void AddProjectileEffect(ProjectileEffect projectileEffect)
        {
            this.ProjectileEffects.Add(projectileEffect);
        }
    }
}
