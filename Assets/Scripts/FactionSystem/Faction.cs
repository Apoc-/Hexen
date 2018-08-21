using System;
using Hexen;

namespace Assets.Scripts.FactionSystem
{
    public abstract class Faction
    {
        protected string Name;
        protected string Description;
        public Factions FactionName;
        public int Standing = 0;

        private FactionTowerContainer towers;

        protected Faction(string name, string description, Factions factionName)
        {
            Name = name;
            Description = description;
            FactionName = factionName;
            towers = new FactionTowerContainer();
        }

        public void AddTower(Tower tower)
        {
            this.towers.AddTower(tower);
        }
    }
}