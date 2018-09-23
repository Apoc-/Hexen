using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ShredderProjectile : Projectile
    {
        private bool aligned = false;
        private float shreddingDuration = 1.5f;
       
        protected override void InitProjectileData()
        {
            
        }

        protected override void InitProjectileEffects()
        {
            ProjectileEffects = new List<ProjectileEffect>();

            AddProjectileEffect(new DamageProjectileEffect());
        }

        public override void Collide(Collider other, Vector3 pos)
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

            if (!TargetReached) return;
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