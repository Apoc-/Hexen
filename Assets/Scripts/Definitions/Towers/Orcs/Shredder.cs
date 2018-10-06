using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.TowerSystem;
using Definitions.ProjectileAttacks;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Definitions.Towers.Orcs
{
    class Shredder : Tower
    {
        public override void InitTowerData()
        {
            Name = "Drok'Tol's Shredder";
            Faction = FactionNames.Orcs;
            Rarity = Rarities.Legendary;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description =
                "This deadly contraption is named after the great Drok'Tol. " +
                "It tosses whirling axes which stay in place to deal continous damage for a certain time.";

            AttackType = typeof(ShredderProjectileAttack);
            ProjectileModelPrefab = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");

            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Orcs/Shredder");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/Shredder");

            WeaponHeight = 0.2f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            
            AddAttribute(new Attribute(
                AttributeName.AttackDamage,
                GameSettings.BaselineTowerDmg[Rarity],
                GameSettings.BaselineTowerDmgInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.AttackSpeed, GameSettings.BaseLineTowerAttackSpeed / 4));
            AddAttribute(new Attribute(AttributeName.AttackRange, GameSettings.BaseLineTowerAttackRange));
        }
    }
}
