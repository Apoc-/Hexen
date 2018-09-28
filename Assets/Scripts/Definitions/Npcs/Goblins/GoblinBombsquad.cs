using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Definitions.Npcs
{
    public class GoblinBombsquad : Npc
    {
        private float stunDuration = 4.0f;
        private float stunRadius = 1.0f;

        protected override void InitNpcData()
        {
            this.Name = "Goblin Bombsquad";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinBombsquad");
            this.HealthBarOffset = 0.4f;

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

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1.2f));
        }

        private void StunKiller(Npc npc, Tower killer)
        {
            var towers = TargetingHelper.GetTowersInRadius(transform.position, stunRadius);
            
            if (killer == null) return;

            killer.Stun(stunDuration, this);
        }
    }
}
