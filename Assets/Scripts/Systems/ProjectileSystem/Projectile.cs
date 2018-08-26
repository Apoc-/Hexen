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

        private void Awake()
        {
            this.InitProjectile();
            this.InitProjectileData();
        }

        protected abstract void InitProjectileData();

        protected abstract void InitProjectile();

        private void OnTriggerEnter(Collider other)
        {
            Collide(other);
        }

        protected virtual void Collide(Collider other)
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
    }
}
