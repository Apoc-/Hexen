using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ShredderProjectile : DirectProjectile
    {
        private bool aligned = false;
        private float shreddingDuration = 1.5f;
       
        protected override void InitProjectileData()
        {
            Speed = 30;
        }

        protected override void InitProjectile()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
        }

        public override void Collide(Collider other)
        {
            var target = other.gameObject.GetComponentInParent<Npc>();

            if (target != null)
            {
                ProjectileEffects.ForEach(effect => effect.OnHit(Source, target));
            }
        }

        protected override void UpdateTransform()
        {
            base.UpdateTransform();

            if (!targetReached) return;
            if (!aligned) Align();

            StartShredding();

            this.transform.Rotate(Vector3.forward, 40.0f);
        }

        private void Align()
        {
            this.transform.rotation = Quaternion.identity * Quaternion.Euler(90f, 0f, 0f);
            aligned = true;
        }

        private void StartShredding()
        {
            Invoke("StopShredding", shreddingDuration);
        }

        private void StopShredding()
        {
            Destroy(gameObject);
        }
    }
}