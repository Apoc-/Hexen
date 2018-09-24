using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.MapSystem
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
