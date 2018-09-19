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
            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Legendary;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description =
                "Deals bonus damage depending on the amount of gold you have. Has a chance to enable a new random tower. " +
                "Has \"Got some coin?\".";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Vault");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(LaurinsVaultProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

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
