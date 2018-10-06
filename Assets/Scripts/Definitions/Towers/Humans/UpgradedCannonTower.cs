using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.TowerSystem;
using Definitions.ProjectileAttacks;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Definitions.Towers.Humans
{
    class UpgradedCannonTower : Tower
    {
        public override void InitTowerData()
        {
            Name = "Upgraded CannonTower";
            Faction = FactionNames.Humans;
            Rarity = Rarities.Legendary;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "A tower that shoots stronger explosive projectiles";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Humans/Cannon");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/CannonTower");

            AttackType = typeof(CannonProjectileAttack);
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
