using System;
using System.Collections.Generic;
using Hexen;
using Hexen.GameData;

namespace Assets.Scripts.FactionSystem
{
    public abstract class Faction
    {
        protected string Name;
        protected string Description;
        public FactionNames FactionName;
        private int standing = 0;

        private readonly Dictionary<TowerRarities, List<Tower>> towers;

        protected Faction(string name, string description, FactionNames factionName)
        {
            Name = name;
            Description = description;
            FactionName = factionName;

            towers = new Dictionary<TowerRarities, List<Tower>>
            {
                { TowerRarities.Common, new List<Tower>() },
                { TowerRarities.Uncommon, new List<Tower>() },
                { TowerRarities.Rare, new List<Tower>() },
                { TowerRarities.Legendary, new List<Tower>() }
            };
        }

        public void AddTower(Tower tower)
        {
            this.towers[tower.Rarity].Add(tower);
        }

        public List<Tower> GetAvailableTowers()
        {
            var availableTowers = new List<Tower>();

            if (standing >= 1)
            {
                availableTowers.AddRange(towers[TowerRarities.Common]);
            }

            if (standing >= 2)
            {
                availableTowers.AddRange(towers[TowerRarities.Uncommon]);
            }

            if (standing >= 3)
            {
                availableTowers.AddRange(towers[TowerRarities.Rare]);
            }

            if (standing >= 4)
            {
                availableTowers.AddRange(towers[TowerRarities.Legendary]);
            }

            return availableTowers;
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
    }
}