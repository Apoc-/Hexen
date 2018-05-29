using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hexen
{
    public class ExplosiveProjectile : Projectile
    {
        public float ExplosionRadius = 1.0f;
        public float ExplosionSplashDamageFaktor = 0.5f;

        protected override void Collide(Collision collision)
        {
            var npc = collision.gameObject.GetComponentInParent<Npc>();

            if (npc != null)
            {               
                var collidersInRadius = new List<Collider>(Physics.OverlapSphere(npc.transform.position, ExplosionRadius));

                foreach (var collider in collidersInRadius)
                {
                    var splashNpc = collider.transform.parent.GetComponent<Npc>();

                    if (splashNpc == null) continue;
                    if (splashNpc == npc) continue;

                    splashNpc.DealDamage(this, ExplosionSplashDamageFaktor);
                }

                npc.DealDamage(this);
                Destroy(this.gameObject);
            }
        }
    }
}
