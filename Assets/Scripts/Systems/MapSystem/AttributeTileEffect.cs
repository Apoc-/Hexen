using Systems.AttributeSystem;
using Systems.NpcSystem;

namespace Systems.MapSystem
{
    public class AttributeTileEffect : TileEffect
    {
        private readonly AttributeEffect effect;

        public AttributeTileEffect(Tile tile, AttributeEffect effect) : base(tile)
        {
            this.effect = effect;
        }

        public override void ApplyEffectToNpc(Npc enteringNpc)
        {
            var attr = enteringNpc.Attributes[effect.AffectedAttributeName];
            attr.AddAttributeEffect(effect);
        }
    }
}