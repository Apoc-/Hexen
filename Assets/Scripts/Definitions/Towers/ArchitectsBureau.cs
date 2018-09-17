using System.Linq;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;
using AttributeName = Assets.Scripts.Systems.AttributeSystem.AttributeName;
using Random = System.Random;

namespace Assets.Scripts.Definitions.Towers
{
    class ArchitectsBureau : Tower, AttributeEffectSource
    {
        private Random rng = new Random();

        public override void InitTower()
        {
            Name = "Architect's Bureau";
            Description = "This Building enforces nearby towers with a small damage bonus whenever the player earns gold. Has \"Got some coin?\".";
            GoldCost = 40;
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Architect");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(ArchitectsBureauProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            WeaponHeight = 0.4f;

            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Uncommon;

            GameManager.Instance.Player.OnGainGold += EnforceNearbyTower;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 1.5f, 0.0f));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 3f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.0f));
            AddAttribute(new Attribute(AttributeName.AuraRange, 1.0f, 0.04f));
        }

        public void EnforceNearbyTower(int goldAmount)
        {
            var colliders = GetCollidersInRadius(Attributes[AttributeName.AuraRange].Value);
            var targets = colliders
                .Select(c => c.GetComponentInParent<Tower>())
                .Where(e => e != null)
                .ToList();

            targets.Add(this);

            var tower = targets[rng.Next(targets.Count)];

            if (tower.HasAttribute(AttributeName.AttackDamage))
            {
                var effect = new AttributeEffect(0.1f, AttributeName.AttackDamage, AttributeEffectType.Flat, this);
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
