using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class BoomstickBlacksmithy : Tower
    {
        public override void InitTowerData()
        {
            Name = "Boomstick Blacksmithy";
            Faction = FactionNames.Goblins;
            Rarity = Rarities.Rare;
            GoldCost = GameSettings.BaselineTowerPrice[Rarity];

            Description = "Uses Boomsticks for forging Weaponry... makes for big boom. Deals a huge amount of damage in a big radius once in a while.";
            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Goblins/BoomstickBlacksmithy");
            ModelPrefab = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            WeaponHeight = 0.4f;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.AttackDamage, 
                GameSettings.BaselineTowerDmg[Rarity],
                GameSettings.BaselineTowerDmgInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.AttackRange, GameSettings.BaseLineTowerAttackRange * 2.5f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, GameSettings.BaseLineTowerAttackSpeed / 10));
        }

        protected override void Attack(bool triggering = true)
        {
            //Does not attack "regulary"
            var se = new ParticleEffectData("BlacksmithyExplosion", gameObject, new Vector3(0, WeaponHeight, 0), 5);
            GameManager.Instance.SpecialEffectManager.PlayParticleEffect(se);

            var r = GetAttributeValue(AttributeName.AttackRange);
            var npcs = TargetingHelper.GetNpcsInRadius(transform.position, r);
            npcs.ForEach(npc => { npc.DealDamage(GetAttributeValue(AttributeName.AttackDamage), this); });
        }
    }
}
