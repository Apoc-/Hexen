using System.Collections.Generic;
using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.TowerSystem;
using Definitions.ProjectileAttacks;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;
using AttributeName = Systems.AttributeSystem.AttributeName;

namespace Definitions.Towers.Dwarfs
{
    class TaxCollectionOffice : Tower
    {
        private List<Tower> _towers = new List<Tower>();
        private int _checkTickInterval = 10;
        private int _checkTick;

        public override void InitTowerData()
        {
            Name = "Tax Collection Office";
            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Rare;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description =
                "Progression Tax: Whenever a nearby tower levels up, the player gets gold and the Collector gets xp equal to the level.";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Office");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            AttackType = typeof(TaxCollectionOfficeProjectileAttack);
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
            AddAttribute(new Attribute(
                AttributeName.AuraRange, 
                GameSettings.BaseLineTowerAuraRange,
                GameSettings.BaseLineTowerAuraRangeInc, 
                LevelIncrementType.Flat));
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();

            _checkTick += 1;
            if (_checkTick >= _checkTickInterval)
            {
                _checkTick = 0;
                RefreshNearbyTowers();
            }
        }

        private void RefreshNearbyTowers()
        {
            UnsubscribeTowerEvents();

            var radius = Attributes[AttributeName.AuraRange].Value;
            _towers = TargetingHelper.GetTowersInRadius(transform.position, radius);
            
            _towers.ForEach(tower => { tower.OnLevelUp += HandleTowerLevelUp; });
        }

        private void HandleTowerLevelUp(int level)
        {
            PlayParticleEffectAboveTower("CoinExplosion", 3);

            GameManager.Instance.Player.IncreaseGold(level);
            GiveXP(level);
        }

        public override void Remove()
        {
            UnsubscribeTowerEvents();

            base.Remove();
        }

        private void UnsubscribeTowerEvents()
        {
            _towers.ForEach(tower => { tower.OnLevelUp -= HandleTowerLevelUp; });
        }
    }
}
