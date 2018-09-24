using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Systems.MapSystem
{
    public class DamageTileEffect : TileEffect
    {
        private readonly float damage;
        private readonly Tower source;

        public DamageTileEffect(Tile tile, float damage, Tower source) : base(tile)
        {
            this.damage = damage;
            this.source = source;
        }

        public override void ApplyEffectToNpc(Npc enteringNpc)
        {
            enteringNpc.DealDamage(damage, source);
        }
    }
}