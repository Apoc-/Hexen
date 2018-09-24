using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.SfxSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class BoomstickArtillery : Tower
    {
        public override void InitTowerData()
        {
            Name = "The Great Boomstick Artillery";
            Faction = FactionNames.Goblins;
            Rarity = Rarities.Common;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "Huge Range with good splash and damage. Moat. At least if you ask the Goblins.";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Goblins/BoomstickArtillery");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(GoblinArtilleryProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");

            WeaponHeight = 0.4f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.AttackDamage, 
                GameSettings.BaselineTowerDmg[Rarity],
                GameSettings.BaselineTowerDmgInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.AttackRange, GameSettings.BaseLineTowerAttackRange * 2.5f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, GameSettings.BaseLineTowerAttackSpeed / 4));
        }
    }
}
