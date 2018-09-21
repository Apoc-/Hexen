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
    public class GoblinBombsquad : Npc
    {
        private float stunDuration = 4.0f;
        private float stunRadius = 1.0f;

        protected override void InitNpcData()
        {
            this.Name = "Goblin Bombsquad";
            this.Model = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinBombsquad");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Rare;
            Faction = FactionNames.Goblins;

            OnDeath += Explode;
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

        private void Explode(Npc npc)
        {
            var towers = GetTowersInRadius(stunRadius);
            var tower = GetRandomTower(towers);

            if (tower == null) return;

            tower.Stun(stunDuration, this);
        }

        private Tower GetRandomTower(List<Tower> towers)
        {
            if (towers.Count == 0) return null;

            var rng = new Random();
            var n = rng.Next(towers.Count);

            return towers[n];
        }
    }
}
