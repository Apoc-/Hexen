using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class HasteAuraTower : AuraTower
    {
        public override void InitTower()
        {
            Name = "Haste Aura Tower";
            Description = "A tower that deals very low damage, but increases the attackspeed of towers in its range.";
            GoldCost = 100;
            ProjectileType = typeof(BoltProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Bolt");
            Icon = Resources.Load<Sprite>("UI/Icons/DefaultTowerIcon");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/HasteAuraTowerModel");
            AuraEffect =
                new AuraEffect(new AttributeEffect(0.1f, AttributeName.AttackSpeed, AttributeEffectType.PercentMul,
                    this));

            WeaponHeight = 1;

            Faction = FactionNames.Humans;
            Rarity = TowerRarities.Common;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 1.5f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 2.0f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.5f, 0.04f, LevelIncrementType.Percentage));
        }
    }
}
