using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hexen;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen.GameData.Towers
{
    class CannonTower : Tower
    {
        public override void InitTower()
        {
            Name = "CannonTower";
            Description = "A tower that shoots explosive projectiles";
            GoldCost = 20;
            Projectile = Resources.Load<Projectile>("Prefabs/Projectiles/Bomb");
            Icon = Resources.Load<Sprite>("UI/Icons/CannonTowerIcon");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/CannonTowerModel");

            WeaponHeight = 1;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 1.75f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 5, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 0.75f, 0.04f, LevelIncrementType.Percentage));
        }
    }
}
