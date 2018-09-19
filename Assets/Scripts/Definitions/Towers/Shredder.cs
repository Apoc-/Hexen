using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class Shredder : Tower
    {
        public override void InitTower()
        {
            Name = "Drok'Tol's Shredder";
            Description =
                "This deadly contraption is named after the great Drok'Tol. " +
                "It tosses whirling axes which stay in place to deal continous damage for a certain time.";
            GoldCost = 500;

            ProjectileType = typeof(ShredderProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Orcs/Shredder");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/Shredder");

            WeaponHeight = 0.2f;
            Faction = FactionNames.Orcs;
            Rarity = Rarities.Legendary;
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
