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
        private string _name;
        private string _description;
        public readonly FactionNames FactionName;
        public readonly FactionNames OpponentFactionName;
        private int _standing;

        private readonly Dictionary<Rarities, List<Tower>> _towers;
        private readonly Dictionary<Rarities, List<Npc>> _npcs;

        protected Faction(
            string name, 
            string description, 
            FactionNames factionName, 
            FactionNames opponentFactionName = FactionNames.Void)
        {
            _name = name;
            _description = description;
            FactionName = factionName;
            OpponentFactionName = opponentFactionName;

            _towers = new Dictionary<Rarities, List<Tower>>
            {
                { Rarities.Common, new List<Tower>() },
                { Rarities.Uncommon, new List<Tower>() },
                { Rarities.Rare, new List<Tower>() },
                { Rarities.Legendary, new List<Tower>() },
                { Rarities.None, new List<Tower>() }
            };

            _npcs = new Dictionary<Rarities, List<Npc>>
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
            _towers[tower.Rarity].Add(tower);
        }

        public void AddNpc(Npc npc)
        {
            _npcs[npc.Rarity].Add(npc);
        }

        public List<Tower> GetAvailableTowers()
        {
            var availableTowers = new List<Tower>();

            if (_standing >= 1)
            {
                availableTowers.AddRange(_towers[Rarities.Common]);
            }

            if (_standing >= 2)
            {
                availableTowers.AddRange(_towers[Rarities.Uncommon]);
            }

            if (_standing >= 3)
            {
                availableTowers.AddRange(_towers[Rarities.Rare]);
            }

            if (_standing >= 4)
            {
                availableTowers.AddRange(_towers[Rarities.Legendary]);
            }

            return availableTowers;
        }

        public List<Npc> GetNpcsByRarity(Rarities rarity)
        {
            return _npcs[rarity];
        }

        public List<Tower> GetTowersByRarity(Rarities rarity)
        {
            return _towers[rarity];
        }

        public T GetNpc<T>() where T : Npc
        {
            Npc foundNpc = null;

            foreach (var npcsInRarity in _npcs.Values)
            {
                foundNpc = npcsInRarity.FirstOrDefault(npc => npc is T);
            }

            var castedNpc = foundNpc as T;
            if (castedNpc == null) throw new ArgumentOutOfRangeException();

            return castedNpc;
        }

        public void IncreaseStanding()
        {
            _standing += 1;
            GameManager.Instance.FactionManager.UpdateAvailableTowers();
        }

        public void DecreaseStanding()
        {
            _standing -= 1;
            GameManager.Instance.FactionManager.UpdateAvailableTowers();
        }

        public int GetStanding()
        {
            return _standing;
        }
    }
}