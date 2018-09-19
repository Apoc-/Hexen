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
        public override void InitTower()
        {
            Name = "War Banner";
            Description = "A War Banner that increases the damage of nearby towers. This tower does not attack!";
            GoldCost = 100;

            //ProjectileType = typeof(ArrowProjectile);
            //ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Orcs/Banner");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/Banner");
            AuraEffects.Add(new AuraEffect(
                attributeEffect: new AttributeEffect(0.25f, AttributeName.AttackDamage, AttributeEffectType.PercentMul, this), 
                affectsTowers: true, 
                affectsNpcs: false));

            WeaponHeight = 1;

            Faction = FactionNames.Orcs;
            Rarity = Rarities.Rare;
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
