﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using Assets.Scripts.Systems.WaveSystem;
using UnityEngine;
using AttributeName = Assets.Scripts.Systems.AttributeSystem.AttributeName;

namespace Assets.Scripts.Definitions.Npcs
{
    public class OrcCommander : Npc
    {
        private float secondWindChance = 0.5f;
        private float secondWindHealthFactor = 0.5f;
        private List<Npc> affectedNpcs;

        protected override void InitNpcData()
        {
            this.Name = "Orc Commander";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Orcs/OrcCommander");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Rare;
            Faction = FactionNames.Orcs;

            if (this.isSpawned)
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

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1f));
        }

        private void AffectNpc(Npc npc)
        {
            npc.OnHit += CheckSecondWind;
            affectedNpcs.Add(npc);
        }

        private void CheckSecondWind(NpcHitData hitData, Npc npc)
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

            Debug.Log("|||||||||| Triggered second wind for " + npc.GetHashCode() + " My Health: " + this.CurrentHealth);
        }

        public override void Die(bool silent = false)
        {
            affectedNpcs.ForEach(npc => { npc.OnHit -= CheckSecondWind; });
            GameManager.Instance.WaveSpawner.OnNpcSpawned -= AffectNpc;

            base.Die();
        }
    }
}