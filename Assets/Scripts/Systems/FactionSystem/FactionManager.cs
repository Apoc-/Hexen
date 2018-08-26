using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Factions;
using Assets.Scripts.Definitions.Towers;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.FactionSystem
{
    public class FactionManager : MonoBehaviour
    {
        private Dictionary<FactionNames, Faction> factions;
        private Dictionary<FactionNames, FactionNames> opponents;
        private List<Tower> availableTowers;
        private int registeredTowerCount = 0;
        private int sentAmbassadors = 0;

        public void Initialize()
        {
            this.InitializeFactions();
            this.InitializeTowers();
        }

        private void InitializeFactions()
        {
            factions = new Dictionary<FactionNames, Faction>();

            AddFaction(new Humans());
            AddFaction(new Orcs());
            AddFaction(new Elves());
        }

        private void InitializeTowers()
        {
            RegisterTower<ArrowTower>();
            RegisterTower<CannonTower>();
            RegisterTower<MundaneBunker>();
            RegisterTower<SiegeCatapult>();
            RegisterTower<WarBanner>();
            RegisterTower<Shredder>();

            Debug.Log("Registered " + registeredTowerCount + " Towers.");
            UpdateAvailableTowers();
        }

        private void RegisterTower<T>() where T : Tower
        {
            GameObject go = new GameObject();
            Tower tower = go.AddComponent<T>();

            go.name = tower.Name;
            go.transform.parent = transform;
            go.SetActive(false);

            factions[tower.Faction].AddTower(tower);
            registeredTowerCount += 1;

        }

        private void AddFaction(Faction faction)
        {
            this.factions[faction.FactionName] = faction;
        }

        public Faction GetFactionByName(FactionNames factionName)
        {
            return this.factions[factionName];
        }

        public List<Tower> GetAvailableTowers()
        {
            return availableTowers;
        }

        public void UpdateAvailableTowers()
        {
            availableTowers = new List<Tower>();

            factions.Values.ToList().ForEach(faction =>
            {
                availableTowers.AddRange(faction.GetAvailableTowers());
            });
        }

        public void SendAmbassador(FactionNames factionName)
        {
            var gm = GameManager.Instance;
            var faction = this.GetFactionByName(factionName);

            faction.IncreaseStanding();
            gm.Player.DecreaseAmbassadors(1);

            gm.UIManager.FactionPanel.UpdateFactionButtons();

            HandleWar(faction.OpponentFactionName);

            sentAmbassadors += 1;
            if (sentAmbassadors <= 1)
            {
                GameManager.Instance.MakePlayerReady();
            }
        }

        public void HandleWar(FactionNames factionName)
        {
            var faction = GetFactionByName(factionName);

            faction.DecreaseStanding();

            if (faction.GetStanding() == -1)
            {
                GameManager.Instance.UIManager.FactionPanel.ToggleFactionWarButton(factionName);
            }
        }
    }
}