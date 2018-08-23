using Assets.Scripts.Systems.AttributeSystem;

namespace Assets.Scripts.Definitions.Runes
{
    public class RuneA : Rune
    {
        public override void InitRuneData()
        {
            base.InitRuneData();

            AddAttributeEffect(new AttributeEffect(10, AttributeName.AttackDamage, AttributeEffectType.Flat, this));
        }
    }
}