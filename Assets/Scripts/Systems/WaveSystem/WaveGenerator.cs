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
                        packs.Add(GenerateCommonPack(faction, wave));
                        break;
                    case -2:
                        //uncommon
                        break;
                    case -3:
                        //rare
                        break;
                    case -4:
                        //legendary
                        break;
                }
            }
            
            return packs;
        }

        private WavePack GenerateCommonPack(Faction faction, int wave)
        {
            var npcs = faction.GetAvailableNpcsByRarity(Rarities.Common);
            var npc = GetRandomNpc(npcs);
            var pack = new WavePack();

            for (int i = 0; i < 5; i++)
            {
                var clone = Instantiate(npc);

                pack.AddNpc(clone);
                Debug.Log("Adding " + clone.Name + " to pack.");
            }

            return pack;
        }

        private Npc GetRandomNpc(List<Npc> npcs)
        {
            return npcs[rand.Next(npcs.Count)];
        }
    }
}