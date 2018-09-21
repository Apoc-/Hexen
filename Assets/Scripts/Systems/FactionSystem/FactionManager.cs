using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Factions;
using Assets.Scripts.Definitions.Npcs;
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
        private List<Npc> availableNpcs;
        private int registeredTowerCount = 0;
        private int registeredNpcCount = 0;
        private int sentAmbassadors = 0;

        public void Initialize()
        {
            this.InitializeFactions();
            this.InitializeTowers();
            this.InitializeNpcs();
        }

        private void InitializeFactions()
        {
            factions = new Dictionary<FactionNames, Faction>();

            AddFaction(new Humans());
            AddFaction(new Orcs());
            AddFaction(new Elves());
            AddFaction(new Dwarfs());
            AddFaction(new Goblins());
        }

        private void InitializeTowers()
        {
            RegisterTower<ArrowTower>();
            RegisterTower<CannonTower>();
            
            //orcs
            RegisterTower<MundaneBunker>();
            RegisterTower<SiegeCatapult>();
            RegisterTower<WarBanner>();
            RegisterTower<Shredder>();
            
            //elves
            RegisterTower<ArcaneNeedle>();
            RegisterTower<StormCaller>();
            RegisterTower<FrostEmitter>();
            RegisterTower<ArotasObelisk>();

            //dwarfs
            RegisterTower<BuildersShack>();
            RegisterTower<ArchitectsBureau>();
            RegisterTower<TaxCollectionOffice>();
            RegisterTower<LarinsVault>();

            //goblins
            RegisterTower<BoomstickTosser>();
            RegisterTower<FastBoomstickTosser>();
            RegisterTower<BoomstickBlacksmithy>();
            RegisterTower<BoomstickArtillery>();

            Debug.Log("Registered " + registeredTowerCount + " Towers.");
            UpdateAvailableTowers();
        }

        private void InitializeNpcs()
        {
            //orcs
            RegisterNpc<OrcTrooper>();
            RegisterNpc<OrcGrunt>();
            RegisterNpc<OrcCommander>();
            RegisterNpc<DrokTol>();

            //elves
            RegisterNpc<ArcaneApprentice>();
            RegisterNpc<StormGlaive>();
            RegisterNpc<FrostMage>();
            RegisterNpc<Arotas>();

            //goblins
            RegisterNpc<GoblinUnderling>();
            RegisterNpc<CrazedGoblin>();
            RegisterNpc<GoblinBombsquad>();
            RegisterNpc<GoblinKing>();

            //dwarfs
            RegisterNpc<DwarfWorker>();
            RegisterNpc<DwarfArchitect>();
            RegisterNpc<DwarfTaxCollector>();
            RegisterNpc<TreasureMasterLarin>();
            RegisterNpc<GoldCoin>();

            Debug.Log("Registered " + registeredNpcCount + " Npcs.");
            UpdateAvailableNpcs();
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

        private void RegisterNpc<T>() where T : Npc
        {
            GameObject go = new GameObject();
            Npc npc = go.AddComponent<T>();
            npc.InitData();

            go.name = npc.Name;
            go.transform.parent = transform;
            go.SetActive(false);

            factions[npc.Faction].AddNpc(npc);
            registeredNpcCount += 1;
        }

        private void AddFaction(Faction faction)
        {
            this.factions[faction.FactionName] = faction;
        }

        public Faction GetFactionByName(FactionNames factionName)
        {
            return this.factions[factionName];
        }

        public Dictionary<FactionNames, Faction> GetFactions()
        {
            return this.factions;
        }

        public List<Tower> GetAvailableTowers()
        {
            return availableTowers;
        }

        public List<Npc> GetAvailableNpcs()
        {
            return availableNpcs;
        }

        public void UpdateAvailableTowers()
        {
            availableTowers = new List<Tower>();

            factions.Values.ToList().ForEach(faction =>
            {
                availableTowers.AddRange(faction.GetAvailableTowers());
            });
        }

        public void UpdateAvailableNpcs()
        {
            availableNpcs = new List<Npc>();

            factions.Values.ToList().ForEach(faction =>
            {
                availableNpcs.AddRange(faction.GetAvailableNpcs());
            });
        }

        public void SendAmbassador(FactionNames factionName)
        {
            var gm = GameManager.Instance;
            var faction = this.GetFactionByName(factionName);

            faction.IncreaseStanding();
            gm.Player.DecreaseAmbassadors(1);
            gm.Player.AddRandomBuildableTowers(4);

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