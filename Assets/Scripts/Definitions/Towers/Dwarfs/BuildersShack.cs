using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.TowerSystem;
using Definitions.ProjectileAttacks;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Definitions.Towers.Dwarfs
{
    class BuildersShack : Tower
    {
        public override void InitTowerData()
        {
            Name = "Builders Shack";
            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Common;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "A tower that attacks intruders in the hopes of getting some coin. Has \"Got some coin?\".";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Shack");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            AttackType = typeof(ArchitectsBureauProjectileAttack);
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
