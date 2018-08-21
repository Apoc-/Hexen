using System.Collections.Generic;
using Hexen;
using Hexen.GameData.Factions;
using UnityEngine;

namespace Assets.Scripts.FactionSystem
{
    public class FactionManager : MonoBehaviour
    {
        private Dictionary<Factions, Faction> factions;

        private void Awake()
        {
            factions = new Dictionary<Factions, Faction>();

            AddFaction(new Humans());
            AddFaction(new Orcs());
            AddFaction(new Elves());
        }

        private void AddFaction(Faction faction)
        {
            this.factions[faction.FactionName] = faction;
        }

        public void RegisterTower(Tower tower)
        {
            this.factions[tower.Faction].AddTower(tower);
        }
    }
}