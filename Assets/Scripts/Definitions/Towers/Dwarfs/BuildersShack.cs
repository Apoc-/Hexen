using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class BuildersShack : Tower
    {
        public override void InitTower()
        {
            Name = "Builders Shack";
            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Common;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "A tower that attacks intruders in the hopes of getting some coin. Has \"Got some coin?\".";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Shack");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(ArchitectsBureauProjectile);
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
