using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class CannonTower : Tower
    {
        public override void InitTower()
        {
            Name = "CannonTower";
            Description = "A tower that shoots explosive projectiles";
            GoldCost = 20;
            ProjectileType = typeof(CannonProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Bomb");
            Icon = Resources.Load<Sprite>("UI/Icons/CannonTowerIcon");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/CannonTowerModel");

            WeaponHeight = 1;
            Faction = FactionNames.Humans;
            Rarity = TowerRarities.Common;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 1.75f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 5, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 0.75f, 0.04f, LevelIncrementType.Percentage));
        }
    }
}
