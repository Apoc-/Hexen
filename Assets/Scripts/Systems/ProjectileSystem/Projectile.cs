using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class Projectile : MonoBehaviour
    {
        protected List<ProjectileEffect> ProjectileEffects;

        public Tower Source;
        public Npc Target;
        private Vector3 lastTargetPosition;
        private Vector3 estimatedTargetPosition;
        private Vector3 initialTargetPosition;

        protected bool TargetReached = false;

        protected GameObject Model;

        protected float SplashRadius = 0;
        protected float FlightDuration = 0.5f;
        private Vector3 currentVelocity = new Vector3();
        private readonly float gravity = Physics.gravity.y;

        public void InitProjectile(Npc target, Tower source)
        {
            InitProjectileData();
            InitProjectileEffects();
            
            this.Target = target;
            this.Source = source;

            estimatedTargetPosition = Target.GetPositionInTime(FlightDuration);
            currentVelocity = ProjectileHelper.ComputeVelocityToHitTargetAtTime(
                transform.position,
                estimatedTargetPosition,
                gravity,
                FlightDuration);
        }

        protected void FixedUpdate()
        {
            UpdateTransform();
        }

        public Vector3 GetTargetPosition()
        {
            if (Target == null) return lastTargetPosition;

            lastTargetPosition = Target.transform.position;

            return lastTargetPosition;
        }

        protected virtual void UpdateTransform()
        {
            var target = GetTargetPosition();

            if (Vector3.Distance(target, transform.position) > 0.01f)
            {
                Vector3 position = transform.position;
                ProjectileHelper.UpdateProjectile(ref position, ref currentVelocity, gravity, Time.fixedDeltaTime);

                transform.position = position;
            }
            else
            {
                transform.position = target;
            }
            
        }

        protected abstract void InitProjectileData();

        protected abstract void InitProjectileEffects();

        void OnCollisionEnter(Collision collision)
        {
            Collide(collision.collider, collision.contacts[0].point);
        }

        public virtual void Collide(Collider other, Vector3 pos)
        {
            var layer = other.gameObject.layer;
            var npcLayer = LayerMask.NameToLayer("Npcs");
            var tileLayer = LayerMask.NameToLayer("Tiles");

            if (layer != npcLayer && layer != tileLayer) return;

            /*Debug.DrawLine(estimatedTargetPosition, estimatedTargetPosition + Vector3.up, Color.red, 99999);
            Debug.DrawLine(pos, pos + Vector3.up, Color.blue, 99999);
            Debug.DrawLine(Target.transform.position, Target.transform.position + Vector3.up, Color.magenta, 99999);*/

            if (layer == npcLayer)
            {
                var npc = other.gameObject.GetComponentInParent<Npc>();
                ApplyEffectsToTarget(npc);

                if (SplashRadius > 0)
                {
                    ApplyEffectsAroundPosition(npc.transform.position);
                }
            }

            if (layer == tileLayer)
            {
                var tile = other.gameObject.GetComponent<Tile>();
                if (tile == Source.Tile) return;

                var splash = SplashRadius > 0 && tile.TileType != TileType.Void && tile.TileType != TileType.Water;

                if (splash)
                {
                    ApplyEffectsAroundPosition(tile.GetTopCenter());
                }
            }

            Destroy(gameObject);
        }

        protected void ApplyEffectsAroundPosition(Vector3 position)
        {
            var collidersInRadius = new List<Collider>(Physics.OverlapSphere(position, SplashRadius));

            foreach (var col in collidersInRadius)
            {
                var target = col.transform.parent.GetComponent<Npc>();

                if (target == null) continue;

                ProjectileEffects.ForEach(effect => effect.OnHit(Source, target));
            }
        }

        protected void ApplyEffectsToTarget(Npc target)
        {
            ProjectileEffects.ForEach(effect => effect.OnHit(Source, target));
        }

        protected void AddProjectileEffect(ProjectileEffect projectileEffect)
        {
            this.ProjectileEffects.Add(projectileEffect);
        }
    }
}