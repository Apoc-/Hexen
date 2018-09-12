using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class ArcaneNeedle : Tower
    {
        public override void InitTower()
        {
            Name = "Arcane Needle";
            Description = "A tower that attacks NPCs with arcane projectiles. Has Magical Affinity.";
            GoldCost = 15;
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Elves/Needle");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(ElvesProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            WeaponHeight = 0.4f;

            Faction = FactionNames.Elves;
            Rarity = TowerRarities.Common;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 2.5f));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 1.5f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.5f));
        }
    }
}
