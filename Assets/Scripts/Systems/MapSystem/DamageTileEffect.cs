using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Systems.MapSystem
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