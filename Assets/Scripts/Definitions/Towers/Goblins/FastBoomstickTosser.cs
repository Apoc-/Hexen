using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class FastBoomstickTosser : Tower
    {
        private int tossComboCount = 0;

        public override void InitTowerData()
        {
            Name = "Fast Boomstick Tosser";
            Faction = FactionNames.Goblins;
            Rarity = Rarities.Uncommon;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "Also throws boomsticks at npcs with a chance to throw another one with a chance to throw another one and another one... Has a small splash.";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Goblins/FastTosser");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(GoblinProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");

            WeaponHeight = 0.4f;

            OnAttack += MultiToss;
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

        private void MultiToss(Npc target)
        {
            var p = 0.5f;

            if (MathHelper.RandomFloat() <= p)
            {
                Fire();
                tossComboCount++;
            }
            else
            {
                if (tossComboCount == 0) return;
                
                var pos = transform.position;
                pos.y += Height;
                
                GameManager.Instance.SfxManager.PlayTextEffect(tossComboCount + " x Combo", pos, 1.5f, 1.75f, Color.red);
                tossComboCount = 0;
            }
        }
    }
}
