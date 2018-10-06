using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.TowerSystem;
using Definitions.ProjectileAttacks;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;
using AttributeName = Systems.AttributeSystem.AttributeName;

namespace Definitions.Towers.Dwarfs
{
    class LarinsVault : Tower
    {
        public override void InitTowerData()
        {
            Name = "Larins Vault";
            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Legendary;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description =
                "Deals bonus damage depending on the amount of gold you have. Has a chance to enable a new random tower. " +
                "Has \"Got some coin?\".";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Vault");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            AttackType = typeof(LaurinsVaultProjectileAttack);
            ProjectileModelPrefab = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");

            WeaponHeight = 0.4f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            
            AddAttribute(new Attribute(
                AttributeName.AttackDamage,
                GameSettings.BaselineTowerDmg[Rarity],
                GameSettings.BaselineTowerDmgInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.AttackSpeed, GameSettings.BaseLineTowerAttackSpeed));
            AddAttribute(new Attribute(AttributeName.AttackRange, GameSettings.BaseLineTowerAttackRange));
        }
    }
}
