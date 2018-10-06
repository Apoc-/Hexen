using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.TowerSystem;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Definitions.Towers.Orcs
{
    class WarBanner : AuraTower
    {
        public override void InitTowerData()
        {
            Name = "War Banner";
            Faction = FactionNames.Orcs;
            Rarity = Rarities.Rare;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];
            
            Description = "A War Banner that increases the damage of nearby towers. This tower does not attack!";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Orcs/Banner");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/Banner");
            AuraEffects.Add(new AuraEffect(
                attributeEffect: new AttributeEffect(0.25f, AttributeName.AttackDamage, AttributeEffectType.PercentMul, this), 
                affectsTowers: true, 
                affectsNpcs: false));

            WeaponHeight = 1;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.AuraRange,
                GameSettings.BaseLineTowerAuraRange,
                GameSettings.BaseLineTowerAuraRangeInc,
                LevelIncrementType.Flat));
        }
    }
}
