using Systems.NpcSystem;

namespace Systems.MapSystem
{
    public abstract class TileEffect
    {
        private Tile tile;

        protected TileEffect(Tile tile)
        {
            this.tile = tile;
        }


        public abstract void ApplyEffectToNpc(Npc enteringNpc);
    }
}
