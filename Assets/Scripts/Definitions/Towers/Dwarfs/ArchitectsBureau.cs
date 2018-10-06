using Systems.AttackSystem;
using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.TowerSystem;
using Definitions.ProjectileAttacks;
using UnityEngine;
using Attribute = Systems.AttributeSystem.Attribute;
using AttributeName = Systems.AttributeSystem.AttributeName;
using Random = System.Random;

namespace Definitions.Towers.Dwarfs
{
    class ArchitectsBureau : Tower, IAttributeEffectSource
    {
        private Random _rng = new Random();

        public override void InitTowerData()
        {
            Name = "Architect's Bureau";
            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Uncommon;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "This Building enforces nearby towers with a small damage bonus whenever the player earns gold. Has \"Got some coin?\".";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Architect");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            AttackType = typeof(ArchitectsBureauProjectileAttack);
            ProjectileModelPrefab = Resources.Load<GameObject>("Prefabs/ProjectileModels/Default");

            WeaponHeight = 0.4f;

            GameManager.Instance.Player.OnGainGold += EnforceNearbyTower;
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
            AddAttribute(new Attribute(AttributeName.AuraRange,GameSettings.BaseLineTowerAuraRange));
        }

        public void EnforceNearbyTower(int goldAmount)
        {
            var range = GetAttributeValue(AttributeName.AuraRange);
            var towers = TargetingHelper.GetTowersInRadius(transform.position, range);

            towers.Add(this);

            var tower = towers[_rng.Next(towers.Count)];

            if (tower.HasAttribute(AttributeName.AttackDamage))
            {
                var effect = new AttributeEffect(0.5f, AttributeName.AttackDamage, AttributeEffectType.Flat, this);
                tower.Attributes[AttributeName.AttackDamage].AddAttributeEffect(effect);
            }
        }

        public override void Remove()
        {
            GameManager.Instance.Player.OnGainGold -= EnforceNearbyTower;

            base.Remove();
        }
    }
}
