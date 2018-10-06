using Systems.NpcSystem;

namespace Systems.MapSystem
{
    public abstract class TileEffect
    {
        // ReSharper disable once NotAccessedField.Local
        private Tile _tile;

        protected TileEffect(Tile tile)
        {
            _tile = tile;
        }


        public abstract void ApplyEffectToNpc(Npc enteringNpc);
    }
}
