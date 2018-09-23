using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class StormCallerProjectile : Projectile
    {
        private List<Npc> hitNpcs = new List<Npc>();

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
            var collisionNpc = other.gameObject.GetComponentInParent<Npc>();

            if (collisionNpc != null && !hitNpcs.Contains(collisionNpc))
            {
                ProjectileEffects.ForEach(effect => effect.OnHit(Source, collisionNpc));

                hitNpcs.Add(collisionNpc);

                //get nearest next target
                var collidersInJumpRange = collisionNpc.GetCollidersInRadius(10.0f, GameSettings.NpcLayerMask);

                var minDistance = float.MaxValue;
                this.Target = null;
                foreach (var col in collidersInJumpRange)
                {
                    var npc = col.transform.parent.GetComponent<Npc>();
                    if (npc == null || npc == collisionNpc || hitNpcs.Contains(npc)) continue;

                    var dist = Vector3.Distance(npc.transform.position, collisionNpc.transform.position);
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        this.Target = npc;
                    }
                }
            }

            if (this.hitNpcs.Count > 3 || this.Target == null) Destroy(gameObject);
        }
    }
}