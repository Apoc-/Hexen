using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

namespace Assets.Scripts.Systems.WaveSystem
{
    public class WaveGenerator : MonoBehaviour
    {
        private Random rand = new Random();
        private Dictionary<Faction, int> legendarySpawned = new Dictionary<Faction, int>();
        private bool initialized = false;
        

        public Wave GenerateWave(int waveNumber)
        {
            if (!initialized) Initialize();

            var finish = (waveNumber == 25);

            var packs = GeneratePacks(waveNumber, finish);

            return new Wave(packs);
        }

        private void Initialize()
        {
            var factions = GameManager.Instance.FactionManager.GetFactions();

            foreach (var faction in factions.Values)
            {
                legendarySpawned[faction] = 0;
            }

            initialized = true;
        }

        private List<WavePack> GeneratePacks(int wave, bool finish = false)
        {
            var packs = new List<WavePack>();
            var fm = GameManager.Instance.FactionManager;

            var factions = fm.GetFactions().Values
                .Where(value => value.GetStanding() < 0)
                .OrderBy(value => value.GetStanding())
                .ToList();

            foreach (var faction in factions)
            {
                var standing = faction.GetStanding();
                var count = ((wave - 1) % 3) + 1;

                if (finish) count = 3;

                if (standing == -1)
                {
                    Debug.Log("Generating common Pack for " + faction.FactionName + " with count: " + count);
                    packs.AddRange(GeneratePack(faction, count, GenerateCommonPack));
                }

                if (standing == -2)
                {
                    Debug.Log("Generating uncommon Pack for " + faction.FactionName + " with count: " + count);
                    packs.AddRange(GeneratePack(faction, count, GenerateUncommonPack));
                }

                if (standing == -3)
                {
                    Debug.Log("Generating rare Pack for " + faction.FactionName + " with count: " + count);
                    packs.AddRange(GeneratePack(faction, count, GenerateRarePack));
                }

                if (standing == -4)
                {
                    // always send three rare packs if all three legendaries have been killed
                    if (legendarySpawned[faction] == 3 && !finish)
                    {
                        Debug.Log("Generating three rare Packs for " + faction.FactionName);
                        packs.AddRange(GeneratePack(faction, 3, GenerateRarePack));
                    }
                    else
                    {
                        Debug.Log("Generating legendary Pack for " + faction.FactionName + " with count: " + count);
                        packs.AddRange(GeneratePack(faction, count, GenerateLegendaryPack));
                    }
                }
            }
            
            //name npc gameobjects
            packs.ForEach(pack =>
            {
                pack.Npcs.ForEach(npc => { npc.gameObject.name = npc.Name; });
            });

            return packs;
        }

        private List<WavePack> GeneratePack(Faction faction, int count, Func<Faction, WavePack> packGenerationFunction)
        {
            var packs = new List<WavePack>();
            
            for (int i = 0; i < count; i++)
            {
                packs.Add(packGenerationFunction(faction));
            }

            return packs;
        }

        private WavePack GenerateCommonPack(Faction faction)
        {
            var commons = faction.GetAvailableNpcsByRarity(Rarities.Common);
            var commonNpc = GetRandomNpc(commons);
            var pack = new WavePack();

            //5
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(commonNpc));

            return pack;
        }

        private WavePack GenerateUncommonPack(Faction faction)
        {
            var commons = faction.GetAvailableNpcsByRarity(Rarities.Common);
            var uncommons = faction.GetAvailableNpcsByRarity(Rarities.Uncommon);
            var pack = new WavePack();

            var commonNpc = GetRandomNpc(commons);
            var uncommonNpc = GetRandomNpc(uncommons);

            //5,2
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(uncommonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(uncommonNpc));
            pack.AddNpc(Instantiate(commonNpc));

            return pack;
        }

        private WavePack GenerateRarePack(Faction faction)
        {
            var commons = faction.GetAvailableNpcsByRarity(Rarities.Common);
            var uncommons = faction.GetAvailableNpcsByRarity(Rarities.Uncommon);
            var rares = faction.GetAvailableNpcsByRarity(Rarities.Rare);
            var pack = new WavePack();

            var commonNpc = GetRandomNpc(commons);
            var uncommonNpc = GetRandomNpc(uncommons);
            var rareNpc = GetRandomNpc(rares);

            //5,2,1
            pack.AddNpc(Instantiate(uncommonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(rareNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(uncommonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            pack.AddNpc(Instantiate(commonNpc));
            
            return pack;
        }

        private WavePack GenerateLegendaryPack(Faction faction)
        {
            var legendaries = faction.GetAvailableNpcsByRarity(Rarities.Legendary);
            var pack = new WavePack();

            // only one legendary
            var npc = GetRandomNpc(legendaries);

            pack.AddNpc(Instantiate(npc));

            // remember spawn
            legendarySpawned[faction] += 1;

            return pack;
        }

        private Npc GetRandomNpc(List<Npc> npcs)
        {
            return npcs[rand.Next(npcs.Count)];
        }
    }
}