using System;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Definitions.Towers
{
    class ArotasObelisk : Tower
    {
        private GameObject MeteoriteModel;

        public override void InitTowerData()
        {
            Name = "Arotas' Obelisk";
            Faction = FactionNames.Elves;
            Rarity = Rarities.Legendary;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "Fire and Brimstone shall hail upon thou. Has Magical Affinity.";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Elves/Obelisk");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(ElvesProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");
            MeteoriteModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Meteorite");

            WeaponHeight = 0.4f;

            OnAttack += CheckMeteoriteTrigger;
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

        private void CheckMeteoriteTrigger()
        {
            var p = 0.1f;
            var rnd = Random.value;

            if (rnd > p) return;

            TriggerMeteorite();
        }

        private void TriggerMeteorite()
        {
            var go = Instantiate(MeteoriteModel);

            var projectile = go.AddComponent<ArotasMeteoriteProjectile>();

            projectile.Duration = 4;
            projectile.DmgPerTick = Attributes[AttributeName.AttackDamage].Value / 8;
            projectile.TicksPerSecond = 4;

            projectile.transform.SetParent(this.transform);
            projectile.transform.localPosition = new Vector3(0, WeaponHeight + 20, 0);

            projectile.Target = this.lockedTarget;
            projectile.Source = this;
        }
    }
}
