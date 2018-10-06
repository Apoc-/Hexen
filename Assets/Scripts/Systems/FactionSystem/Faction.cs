using System;
using System.Collections.Generic;
using System.Linq;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Systems.FactionSystem
{
    public abstract class Faction
    {
        protected string Name;
        protected string Description;
        public FactionNames FactionName;
        public FactionNames OpponentFactionName;
        private int standing = 0;

        private readonly Dictionary<Rarities, List<Tower>> towers;
        private readonly Dictionary<Rarities, List<Npc>> npcs;

        protected Faction(
            string name, 
            string description, 
            FactionNames factionName, 
            FactionNames opponentFactionName = FactionNames.Void)
        {
            this.Name = name;
            this.Description = description;
            this.FactionName = factionName;
            this.OpponentFactionName = opponentFactionName;

            towers = new Dictionary<Rarities, List<Tower>>
            {
                { Rarities.Common, new List<Tower>() },
                { Rarities.Uncommon, new List<Tower>() },
                { Rarities.Rare, new List<Tower>() },
                { Rarities.Legendary, new List<Tower>() },
                { Rarities.None, new List<Tower>() }
            };

            npcs = new Dictionary<Rarities, List<Npc>>
            {
                { Rarities.Common, new List<Npc>() },
                { Rarities.Uncommon, new List<Npc>() },
                { Rarities.Rare, new List<Npc>() },
                { Rarities.Legendary, new List<Npc>() },
                { Rarities.None, new List<Npc>() }
            };
        }

        public void AddTower(Tower tower)
        {
            this.towers[tower.Rarity].Add(tower);
        }

        public void AddNpc(Npc npc)
        {
            this.npcs[npc.Rarity].Add(npc);
        }

        public List<Tower> GetAvailableTowers()
        {
            var availableTowers = new List<Tower>();

            if (standing >= 1)
            {
                availableTowers.AddRange(towers[Rarities.Common]);
            }

            if (standing >= 2)
            {
                availableTowers.AddRange(towers[Rarities.Uncommon]);
            }

            if (standing >= 3)
            {
                availableTowers.AddRange(towers[Rarities.Rare]);
            }

            if (standing >= 4)
            {
                availableTowers.AddRange(towers[Rarities.Legendary]);
            }

            return availableTowers;
        }

        public List<Npc> GetNpcsByRarity(Rarities rarity)
        {
            return npcs[rarity];
        }

        public List<Tower> GetTowersByRarity(Rarities rarity)
        {
            return towers[rarity];
        }

        public T GetNpc<T>() where T : Npc
        {
            Npc foundNpc = null;

            foreach (var npcsInRarity in npcs.Values)
            {
                foundNpc = npcsInRarity.FirstOrDefault(npc => npc is T);
            }

            var castedNpc = foundNpc as T;
            if (castedNpc == null) throw new ArgumentOutOfRangeException();

            return castedNpc;
        }

        public void IncreaseStanding()
        {
            this.standing += 1;
            GameManager.Instance.FactionManager.UpdateAvailableTowers();
        }

        public void DecreaseStanding()
        {
            this.standing -= 1;
            GameManager.Instance.FactionManager.UpdateAvailableTowers();
        }

        public int GetStanding()
        {
            return standing;
        }
    }
}