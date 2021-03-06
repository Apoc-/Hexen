﻿using Systems.AttributeSystem;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Definitions.Npcs.Goblins
{
    public class GoblinBombsquad : Npc
    {
        private float _stunDuration = 4.0f;

        protected override void InitNpcData()
        {
            Name = "Goblin Bombsquad";
            ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinBombsquad");
            HealthBarOffset = 0.4f;

            Rarity = Rarities.Rare;
            Faction = FactionNames.Goblins;

            OnDeath += StunKiller;
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

        private void StunKiller(Npc npc, Tower killer)
        {
            if (killer == null) return;

            killer.Stun(_stunDuration, this);
        }
    }
}
