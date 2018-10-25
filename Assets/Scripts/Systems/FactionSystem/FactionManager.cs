using System;
using System.Collections.Generic;
using System.Linq;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using Definitions.Factions;
using Definitions.Npcs.Dwarfs;
using Definitions.Npcs.Elves;
using Definitions.Npcs.Goblins;
using Definitions.Npcs.Orcs;
using Definitions.Towers.Dwarfs;
using Definitions.Towers.Elves;
using Definitions.Towers.Goblins;
using Definitions.Towers.Humans;
using Definitions.Towers.Orcs;
using UnityEngine;

namespace Systems.FactionSystem
{
    public class FactionManager : MonoBehaviour
    {
        private Dictionary<FactionNames, Faction> _factions;
        private Dictionary<FactionNames, FactionNames> _opponents;
        private List<Tower> _availableTowers;
        private List<Npc> _availableNpcs;
        private int _registeredTowerCount;
        private int _registeredNpcCount;
        private int _sentAmbassadors;

        public void Initialize()
        {
            InitializeFactions();
            InitializeTowers();
            InitializeNpcs();
        }

        private void InitializeFactions()
        {
            _factions = new Dictionary<FactionNames, Faction>();

            AddFaction(new Humans());
            AddFaction(new Orcs());
            AddFaction(new Elves());
            AddFaction(new Dwarfs());
            AddFaction(new Goblins());
        }

        private void InitializeTowers()
        {
            //humans
            RegisterTower<ArrowTower>();
            RegisterTower<CannonTower>();
            RegisterTower<UpgradedArrowTower>();
            RegisterTower<UpgradedCannonTower>();

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

            Debug.Log("Registered " + _registeredTowerCount + " Towers.");
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

            Debug.Log("Registered " + _registeredNpcCount + " Npcs.");
        }

        private void RegisterTower<T>() where T : Tower
        {
            GameObject go = new GameObject();
            Tower tower = go.AddComponent<T>();
            go.layer = LayerMask.NameToLayer("Towers");

            //tower.InitTower();

            //go.name = tower.Name;
            go.name = tower.GetType().Name;
            go.transform.parent = transform;
            go.SetActive(false);
            
            _factions[tower.Faction].AddTower(tower);
            _registeredTowerCount += 1;
        }

        private void RegisterNpc<T>() where T : Npc
        {
            GameObject go = new GameObject();
            Npc npc = go.AddComponent<T>();
            npc.InitData();

            go.name = npc.Name;
            go.transform.parent = transform;
            go.SetActive(false);

            _factions[npc.Faction].AddNpc(npc);
            _registeredNpcCount += 1;
        }

        private void AddFaction(Faction faction)
        {
            _factions[faction.FactionName] = faction;
        }

        public Faction GetFactionByName(FactionNames factionName)
        {
            return _factions[factionName];
        }

        public Dictionary<FactionNames, Faction> GetFactionDictionary()
        {
            return _factions;
        }

        public List<Faction> GetFactions()
        {
            return _factions.Values.ToList();
        }

        //todo remove
        public List<Tower> GetAvailableTowers()
        {
            return _availableTowers;
        }

        public List<Npc> GetFactionNpcsByRarity(FactionNames factionName, Rarities rarity)
        {
            return _factions[factionName].GetNpcsByRarity(rarity);
        }

        public List<Tower> GetFactionTowersByRarity(FactionNames factionName, Rarities rarity)
        {
            return _factions[factionName].GetTowersByRarity(rarity);
        }

        public void UpdateAvailableTowers()
        {
            _availableTowers = new List<Tower>();

            _factions.Values.ToList().ForEach(faction =>
            {
                _availableTowers.AddRange(faction.GetAvailableTowers());
            });
        }

        public void SendAmbassador(FactionNames factionName)
        {
            var gm = GameManager.Instance;
            var faction = GetFactionByName(factionName);

            faction.IncreaseStanding();
            gm.Player.DecreaseAmbassadors(1);

            gm.UIManager.FactionPanel.UpdateFactionButtons();

            HandleWar(faction.OpponentFactionName);

            _sentAmbassadors += 1;
            if (_sentAmbassadors == GameSettings.StartingAmbassadors)
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

        public Faction GetRandomAlliedFactionByStanding()
        {
            bool Predicate(Faction faction) => faction.GetStanding() > 0;
            return GetRandomFactionByStanding(Predicate, 1);
        }

        public Faction GetRandomEnemyFactionByStanding()
        {
            bool Predicate(Faction faction) => faction.GetStanding() < 0;
            return GetRandomFactionByStanding(Predicate, -1);
        }

        private Faction GetRandomFactionByStanding(Predicate<Faction> pred, int factor)
        {
            var fm = GameManager.Instance.FactionManager;
            var factions = fm.GetFactions()
                .Where(faction => pred(faction))
                .ToList();

            var rolledFaction = factions[0];
            var maxRoll = Mathf.NegativeInfinity;

            factions.ForEach(faction =>
            {
                var standing = faction.GetStanding();
                var roll = MathHelper.RandomFloat() * standing * factor;

                if (roll >= maxRoll)
                {
                    maxRoll = roll;
                    rolledFaction = faction;
                }
            });

            return rolledFaction;
        }
    }
}