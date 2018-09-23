using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
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
