using System.Linq;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class StormGlaive : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Storm Glaive";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Elves/StormGlaive");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Uncommon;
            Faction = FactionNames.Elves;

            this.OnHit += TetherDamage;
        }

        private void TetherDamage(Npc npc, NpcHitData hitdata)
        {
            var dmg = hitdata.Dmg;
            var source = hitdata.Source;
            var glaives = GameManager.Instance.WaveSpawner
                .GetCurrentSpawnedNpcs()
                .Where(n => n is StormGlaive)
                .Where(n => n != this)
                .ToList();

            var dmgPerGlaive = dmg / glaives.Count();
            if (dmgPerGlaive < 1) dmgPerGlaive = 1;

            glaives.ForEach(glaive =>
            {
                glaive.DealDamage(dmgPerGlaive, source);
            });

            hitdata.Dmg = dmgPerGlaive;
        }


        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, GameSettings.BaseLineNpcMovementspeed));
        }
    }
}