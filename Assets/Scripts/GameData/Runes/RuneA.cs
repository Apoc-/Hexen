using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen.GameData.Runes
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