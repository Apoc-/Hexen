﻿using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class UpgradedArrowTower : Tower
    {
        public override void InitTowerData()
        {
            Name = "Upgraded Arrow Tower";
            Faction = FactionNames.Humans;
            Rarity = Rarities.Rare;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "A tower that shoots stronger arrows";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Humans/Arrow");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            AttackType = typeof(ArrowProjectileAttack);
            ProjectileModelPrefab = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");

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