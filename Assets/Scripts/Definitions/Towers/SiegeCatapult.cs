using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class SiegeCatapult : Tower
    {
        public override void InitTower()
        {
            Name = "Siege Catapult Tower";
            Description = "A tower that launches projectiles that deal damage in an area. Has Frenzy.";
            GoldCost = 30;

            ProjectileType = typeof(OrcAoeProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Bomb");

            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Orcs/Catapult");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/Catapult");

            WeaponHeight = 0.2f;
            Faction = FactionNames.Orcs;
            Rarity = TowerRarities.Uncommon;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 2.5f));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 6f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.25f));
        }
    }
}
