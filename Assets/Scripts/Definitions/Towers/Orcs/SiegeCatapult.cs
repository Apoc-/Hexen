using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class SiegeCatapult : Tower
    {
        public override void InitTowerData()
        {
            Name = "Siege Catapult Tower";
            Faction = FactionNames.Orcs;
            Rarity = Rarities.Uncommon;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "A tower that launches projectiles that deal damage in an area. Has Frenzy.";

            AttackType = typeof(OrcAoeProjectileAttack);
            ProjectileModelPrefab = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");

            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Orcs/Catapult");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/Catapult");

            WeaponHeight = 0.2f;
            
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
