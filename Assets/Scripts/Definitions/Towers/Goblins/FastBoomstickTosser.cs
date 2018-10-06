using System.Collections;
using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.SpecialEffectSystem;
using Systems.TowerSystem;
using Definitions.ProjectileAttacks;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;

namespace Definitions.Towers.Goblins
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

            AttackType = typeof(GoblinProjectileAttack);
            ProjectileModelPrefab = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");

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
                
                tossComboCount++;
            }
            else
            {
                if (tossComboCount == 0) return;

                StartCoroutine(ExecuteAttacks(tossComboCount));

                var offset = new Vector3(0, Height, 0);
                var textEffect = new TextEffectData(tossComboCount + "x!", 1.5f, GameSettings.CritColor, gameObject, offset, 1.75f);
                GameManager.Instance.SpecialEffectManager.PlayTextEffect(textEffect);
                tossComboCount = 0;
            }
        }

        private IEnumerator ExecuteAttacks(int comboCount)
        {
            for (int i = 0; i < comboCount; i++)
            {
                Attack(false);
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
    }
}
