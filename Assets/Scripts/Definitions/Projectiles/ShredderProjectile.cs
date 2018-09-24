using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ShredderProjectile : Projectile
    {
        private bool aligned = false;
        private float shreddingDuration = 3f;
        private bool targetReached = false;


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
            var tile = other.gameObject.GetComponent<Tile>();

            if (target != null)
            {
                ProjectileEffects.ForEach(effect => effect.OnHit(Source, target));
                tile = target.CurrentTile;
            }

            var axe = new SpecialEffect("shred", tile.gameObject, shreddingDuration);
            var offset = new Vector3(0, 0.2f+tile.GetTileHeight(), 0f);
            GameManager.Instance.SfxManager.PlaySpecialEffect(axe, offset);

            var dmg = Source.Attributes[AttributeName.AttackDamage].Value;
            var tileEffect = new DamageTileEffect(tile, dmg, Source);
            tile.SetTileEffect(tileEffect, shreddingDuration);

            Destroy(gameObject);
        }
    }
}