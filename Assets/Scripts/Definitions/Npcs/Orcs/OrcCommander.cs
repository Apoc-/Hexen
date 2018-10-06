using System.Collections.Generic;
using System.Linq;
using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;
using AttributeName = Systems.AttributeSystem.AttributeName;

namespace Definitions.Npcs.Orcs
{
    public class OrcCommander : Npc
    {
        private float secondWindChance = 0.5f;
        private float secondWindHealthFactor = 0.5f;
        private List<Npc> affectedNpcs;

        protected override void InitNpcData()
        {
            Name = "Orc Commander";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Orcs/OrcCommander");
            HealthBarOffset = 0.4f;

            Rarity = Rarities.Rare;
            Faction = FactionNames.Orcs;

            if (isSpawned)
            {
                affectedNpcs = new List<Npc>();
                CheckForAffectableNpcs();
                GameManager.Instance.WaveSpawner.OnNpcSpawned += AffectNpc;
            }
        }

        private void CheckForAffectableNpcs()
        {
            var waves = GameManager.Instance.WaveSpawner.CurrentSpawnedWaves;
            waves.ForEach(wave =>
            {
                affectedNpcs.AddRange(wave.SpawnedNpcs.Where(npc => npc != this));
            });
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

        private void AffectNpc(Npc npc)
        {
            npc.OnHit += CheckSecondWind;
            affectedNpcs.Add(npc);
        }

        private void CheckSecondWind(Npc npc, NpcHitData hitData)
        {
            //cannot pull himself out of the swamp
            if (npc == this) return;

            var wouldKill = (npc.CurrentHealth - hitData.Dmg) <= 0;
            if ( !wouldKill ) return;

            var rnd = Random.value;
            if (rnd > secondWindChance) return;

            hitData.Dmg = 0;

            var hpAttr = npc.GetAttribute(AttributeName.MaxHealth);
            hpAttr.Value = hpAttr.Value * secondWindHealthFactor;

            npc.Heal();

            Debug.Log("|||||||||| Triggered second wind for " + npc.GetHashCode() + " My Health: " + CurrentHealth);
        }

        public override void Die(bool silent = false)
        {
            affectedNpcs.ForEach(npc => { npc.OnHit -= CheckSecondWind; });
            GameManager.Instance.WaveSpawner.OnNpcSpawned -= AffectNpc;

            base.Die();
        }
    }
}