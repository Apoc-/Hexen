using System.Collections.Generic;
using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.GameSystem;
using Systems.MapSystem;
using Systems.NpcSystem;
using Systems.SpecialEffectSystem;
using Definitions.AttackEffects;
using UnityEngine;

namespace Definitions.ProjectileAttacks
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