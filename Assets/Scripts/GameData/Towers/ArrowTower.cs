using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hexen;
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
            Projectile = Resources.Load<Projectile>("Prefabs/Projectiles/Arrow");
            Icon = Resources.Load<Sprite>("UI/Icons/ArrowTowerIcon");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTowerModel");

            WeaponHeight = 1;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 2, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 2.5f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 2, 0.04f, LevelIncrementType.Percentage));
        }
    }
}
