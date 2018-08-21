using System;
using System.Collections.Generic;
using Hexen;
using Hexen.GameData;

namespace Assets.Scripts.FactionSystem
{
    public class FactionTowerContainer
    {
        private readonly Dictionary<TowerRarities, List<Tower>> towers;

        public FactionTowerContainer()
        {
            towers = new Dictionary<TowerRarities, List<Tower>>
            {
                { TowerRarities.Common, new List<Tower>() },
                { TowerRarities.Uncommon, new List<Tower>() },
                { TowerRarities.Rare, new List<Tower>() },
                { TowerRarities.Legendary, new List<Tower>() }
            };
        }

        internal void AddTower(Tower tower)
        {
            this.towers[tower.Rarity].Add(tower);
        }

        private List<Tower> GetTowersOfRarity(TowerRarities rarity)
        {
            return this.towers[rarity];
        }
    }
}