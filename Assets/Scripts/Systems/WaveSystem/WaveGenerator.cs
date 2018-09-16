using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Systems.WaveSystem
{
    public class WaveGenerator : MonoBehaviour
    {
        private Random rand = new Random();

        public Wave GenerateWave(int waveNumber)
        {
            var packs = GeneratePacks(waveNumber);

            return new Wave(packs);
        }

        private List<WavePack> GeneratePacks(int wave)
        {
            var packs = new List<WavePack>();
            var fm = GameManager.Instance.FactionManager;

            var factions = fm.GetFactions().Values
                .Where(value => value.GetStanding() < 0)
                .OrderBy(value => value.GetStanding())
                .ToList();

            foreach (var faction in factions)
            {
                switch (faction.GetStanding())
                {
                    case -1:
                        Debug.Log("Generating common Pack for " + faction.FactionName);
                        packs.AddRange(GeneratePacks(faction, wave, GenerateCommonPack));
                        break;
                    case -2:
                        Debug.Log("Generating uncommon Pack for " + faction.FactionName);
                        packs.AddRange(GeneratePacks(faction, wave, GenerateUncommonPack));
                        break;
                    case -3:
                        Debug.Log("Generating rare Pack for " + faction.FactionName);
                        packs.AddRange(GeneratePacks(faction, wave, GenerateRarePack));
                        break;
                    case -4:
                        Debug.Log("Generating lgendary Pack for " + faction.FactionName);
                        packs.AddRange(GeneratePacks(faction, wave, GenerateLegendaryPack));
                        break;
                }
            }
            
            return packs;
        }

        private List<WavePack> GeneratePacks(Faction faction, int wave, Func<Faction, WavePack> packGenerationFunction)
        {
            var packs = new List<WavePack>();
            var count = ((wave - 1) % 3) + 1;

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

            return pack;
        }

        private Npc GetRandomNpc(List<Npc> npcs)
        {
            return npcs[rand.Next(npcs.Count)];
        }
    }
}