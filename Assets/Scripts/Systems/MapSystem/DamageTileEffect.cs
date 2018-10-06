using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Systems.MapSystem
{
    public class DamageTileEffect : TileEffect
    {
        private readonly float _damage;
        private readonly Tower _source;

        public DamageTileEffect(Tile tile, float damage, Tower source) : base(tile)
        {
            _damage = damage;
            _source = source;
        }

        public override void ApplyEffectToNpc(Npc enteringNpc)
        {
            enteringNpc.DealDamage(_damage, _source);
        }
    }
}