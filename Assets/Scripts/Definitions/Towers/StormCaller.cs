using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class StormCaller : Tower
    {
        public override void InitTower()
        {
            Name = "Storm Caller";
            Description = "The Storm Caller's attacks jump to nearby enemies. Has Magical Affinity.";
            GoldCost = 30;
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Elves/Caller");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(StormCallerProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            WeaponHeight = 0.4f;

            Faction = FactionNames.Elves;
            Rarity = Rarities.Uncommon;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 2.5f, 0.0f));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 1.5f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.5f));
        }
    }
}
