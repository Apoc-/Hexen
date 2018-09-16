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
            GoldCost = 15;
            ProjectileType = typeof(CannonProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Bomb");
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Humans/Cannon");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/CannonTower");

            WeaponHeight = 0.4f;
            Faction = FactionNames.Humans;
            Rarity = Rarities.Common;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 1.5f));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 4f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.0f));
        }
    }
}
