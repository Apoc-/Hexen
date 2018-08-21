using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AttributeSystem;
using Assets.Scripts.ProjectileSystem;
using Assets.Scripts.FactionSystem;
using Hexen;
using Hexen.GameData.Projectiles;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen.GameData.Towers
{
    class ArrowTower : Tower
    {
        public override void InitTower()
        {
            Name = "Arrow Tower";
            Description = "A tower that shoots arrows";
            GoldCost = 15;            
            Icon = Resources.Load<Sprite>("UI/Icons/ArrowTowerIcon");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTowerModel");

            ProjectileType = typeof(ArrowProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            WeaponHeight = 1;

            Faction = Assets.Scripts.FactionSystem.Factions.Humans;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 2, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 2.5f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 2.0f, 0.04f, LevelIncrementType.Percentage));
        }
    }
}
