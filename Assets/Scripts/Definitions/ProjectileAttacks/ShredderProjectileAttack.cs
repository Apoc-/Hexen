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
    public class ShredderProjectileAttack : ProjectileAttack
    {
        private float shreddingDuration = 3f;

        protected override void InitAttackData()
        {
            
        }

        protected override void InitAttackEffects()
        {
            AttackEffects = new List<AttackEffect>();

            AddAttackEffect(new DamageAttackEffect());
        }

        public override void Collide(Collider other, Vector3 pos)
        {
            var target = other.gameObject.GetComponentInParent<Npc>();
            var tile = other.gameObject.GetComponent<Tile>();

            if (target != null)
            {
                AttackEffects.ForEach(effect => effect.OnHit(Source, target));
                tile = target.CurrentTile;
            }

            var offset = new Vector3(0, 0.2f + tile.GetTileHeight(), 0f);
            var axe = new ParticleEffectData("shred", tile.gameObject, offset, shreddingDuration);
            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(axe);

            var dmg = Source.Attributes[AttributeName.AttackDamage].Value;
            var tileEffect = new DamageTileEffect(tile, dmg, Source);
            tile.SetTileEffect(tileEffect, shreddingDuration);

            Destroy(gameObject);
        }
    }
}