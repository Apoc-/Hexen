using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.MapSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.ProjectileSystem
{
    public abstract class DirectAoeProjectile : DirectProjectile
    {
        public float Radius = 1.0f;

        public override void Collide(Collider other)
        {
            var tile = other.gameObject.GetComponent<Tile>();
            if (tile == null) return;

            if (tile.TileType == TileType.Void || tile.TileType == TileType.Water)
            {
                Destroy(this.gameObject);
                return;
            }


            var collidersInRadius = new List<Collider>(Physics.OverlapSphere(tile.GetTopCenter(), Radius));

            foreach (var collider in collidersInRadius)
            {
                var target = collider.transform.parent.GetComponent<Npc>();

                if (target == null) continue;

                ProjectileEffects.ForEach(effect => effect.OnHit(Source, target));
            }

            Destroy(this.gameObject);
        }
    }
}
