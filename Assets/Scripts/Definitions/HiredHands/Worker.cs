using Systems.AttributeSystem;
using Systems.HiredHandSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.HiredHands
{
    public class Worker : HiredHandItem
    {
        protected override void InitData()
        {
            Icon = Resources.Load<Sprite>("UI/Icons/HiredHands/Worker");
            Rarity = Rarities.Common;
            Cost = 10;
            Name = "Worker";
            Description = "A basic hired hand, increasing damage of the built tower by 10%";
            Type = HiredHandType.Worker;
        }

        protected override void InitAttributeEffects()
        {
            AddAttributeEffect(new AttributeEffect(0.1f, AttributeName.AttackDamage, AttributeEffectType.PercentAdd, this));
        }
    }
}