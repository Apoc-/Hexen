using System.Linq;
using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Elves
{
    public class StormGlaive : Npc
    {
        protected override void InitNpcData()
        {
            Name = "Storm Glaive";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Elves/StormGlaive");
            HealthBarOffset = 0.4f;

            Rarity = Rarities.Uncommon;
            Faction = FactionNames.Elves;

            OnHit += TetherDamage;
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