using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class AoeProjectile : Projectile
    {
        public float Radius = 1.0f;

        protected override void Collide(Collision collision)
        {
            var npc = collision.gameObject.GetComponentInParent<Npc>();

            if (npc == null) return;

            var collidersInRadius = new List<Collider>(Physics.OverlapSphere(npc.transform.position, Radius));

            foreach (var collider in collidersInRadius)
            {
                var target = collider.transform.parent.GetComponent<Npc>();

                if (target == null) continue;

                ProjectileEffects.ForEach(effect => effect.OnHit(Source, target));

                Destroy(this.gameObject);
            }
        }
    }
}
