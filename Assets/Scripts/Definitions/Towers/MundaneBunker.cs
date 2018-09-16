using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class MundaneBunker : Tower
    {
        public override void InitTower()
        {
            Name = "Mundane Bunker";
            Description = "An orcish bunker with frenzy.";
            GoldCost = 15;
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Orcs/Bunker");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/MundaneBunker");

            ProjectileType = typeof(OrcProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            WeaponHeight = 0.2f;

            Faction = FactionNames.Orcs;
            Rarity = Rarities.Common;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 2f));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 2f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.25f));
        }
    }
}
