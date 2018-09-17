using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;
using AttributeName = Assets.Scripts.Systems.AttributeSystem.AttributeName;
using Random = System.Random;

namespace Assets.Scripts.Definitions.Towers
{
    class LarinsVault : Tower
    {
        public override void InitTower()
        {
            Name = "Larins Vault";
            Description =
                "Deals bonus damage depending on the amount of gold you have. Has a chance to enable a new random tower. " +
                "Has \"Got some coin?\".";
            GoldCost = 500;
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Vault");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(LaurinsVaultProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            WeaponHeight = 0.4f;

            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Common;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 1.5f, 0.0f));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 10f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.25f));
        }
    }
}
