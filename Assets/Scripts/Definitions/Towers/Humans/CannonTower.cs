using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
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
            Faction = FactionNames.Humans;
            Rarity = Rarities.Common;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "A tower that shoots explosive projectiles";
            
            ProjectileType = typeof(CannonProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Bomb");
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Humans/Cannon");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/CannonTower");

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
