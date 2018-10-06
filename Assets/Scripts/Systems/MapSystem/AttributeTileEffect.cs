using Systems.AttributeSystem;
using Systems.NpcSystem;

namespace Systems.MapSystem
{
    public class AttributeTileEffect : TileEffect
    {
        private readonly AttributeEffect _effect;

        public AttributeTileEffect(Tile tile, AttributeEffect effect) : base(tile)
        {
            _effect = effect;
        }

        public override void ApplyEffectToNpc(Npc enteringNpc)
        {
            var attr = enteringNpc.Attributes[_effect.AffectedAttributeName];
            attr.AddAttributeEffect(_effect);
        }
    }
}