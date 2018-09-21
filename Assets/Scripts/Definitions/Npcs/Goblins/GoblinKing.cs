using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Definitions.Npcs
{
    public class GoblinKing : Npc
    {
        private float stunDuration = 4.0f;
        private float stunRadius = 3.0f;
        private bool stunTriggered = false;

        protected override void InitNpcData()
        {
            this.Name = "Goblin King";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinKing");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Legendary;
            Faction = FactionNames.Goblins;

            OnHit += CheckExplode;
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

        private void CheckExplode(NpcHitData hitData, Npc npc)
        {
            if (stunTriggered) return;
            if (npc.CurrentHealth > npc.Attributes[AttributeName.MaxHealth].Value / 2.0f) return;
            
            var towers = GetTowersInRadius(stunRadius);

            towers.ForEach(tower =>
            {
                tower.Stun(stunDuration, this);
            });
        }
    }
}
