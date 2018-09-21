using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class Projectile : MonoBehaviour
    {
        protected List<ProjectileEffect> ProjectileEffects;

        public Tower Source;
        public Npc Target;

        protected float Speed;
        protected GameObject Model;

        protected Vector3 Velocity = Vector3.zero;
        protected float VelocityDampeningFactor = 0.5f;

        private void Awake()
        {
            this.InitProjectile();
            this.InitProjectileData();
        }

        protected abstract void InitProjectileData();

        protected abstract void InitProjectile();

        /*private void OnTriggerEnter(Collider other)
        {
            Collide(other);
        }*/

        public virtual void Collide(Collider other)
        {
            var target = other.gameObject.GetComponentInParent<Npc>();

            if (target != null)
            {
                ProjectileEffects.ForEach(effect => effect.OnHit(Source, target));

                Destroy(gameObject);
            }
        }

        protected abstract void UpdateTransform();

        private void FixedUpdate()
        {
            UpdateTransform();
        }

        protected void AddProjectileEffect(ProjectileEffect projectileEffect)
        {
            this.ProjectileEffects.Add(projectileEffect);
        }

        protected void UpdateVelocity()
        {
            if (Velocity == Vector3.zero) return;

            Velocity *= VelocityDampeningFactor;

            if (Velocity.magnitude <= 0.000001) Velocity = Vector3.zero;
        }
    }
}
