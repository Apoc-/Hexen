﻿using System.Collections.Generic;
using Systems.GameSystem;
using Systems.MapSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Systems.AttackSystem
{
    public abstract class ProjectileAttack : AbstractAttack
    {
        private Vector3 _lastTargetPosition;
        private Vector3 _estimatedTargetPosition;
        private Vector3 _initialTargetPosition;

        //protected GameObject Model;

        protected float SplashRadius = 0;
        protected float FlightDuration = 0.7f;
        private Vector3 _currentVelocity;
        private readonly float _gravity = Physics.gravity.y;

        public override void InitAttack(Npc target, Tower source)
        {
            base.InitAttack(target, source);

            if (Target != null)
            {
                _estimatedTargetPosition = Target.GetPositionInTime(FlightDuration);
            }
            
            _currentVelocity = ProjectileHelper.ComputeVelocityToHitTargetAtTime(
                transform.position,
                _estimatedTargetPosition,
                _gravity,
                FlightDuration);
        }

        protected void FixedUpdate()
        {
            UpdateTransform();
        }

        public Vector3 GetTargetPosition()
        {
            if (Target == null) return _lastTargetPosition;

            _lastTargetPosition = Target.transform.position;

            return _lastTargetPosition;
        }

        protected virtual void UpdateTransform()
        {
            var target = GetTargetPosition();

            if (Vector3.Distance(target, transform.position) > 0.01f)
            {
                Vector3 position = transform.position;
                ProjectileHelper.UpdateProjectile(ref position, ref _currentVelocity, _gravity, Time.fixedDeltaTime);

                transform.position = position;
            }
            else
            {
                transform.position = target;
            }
            
        }
        
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

                AttackEffects.ForEach(effect => effect.OnHit(Source, target));
            }
        }
    }
}