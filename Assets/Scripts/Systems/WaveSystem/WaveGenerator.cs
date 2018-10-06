using System.Collections.Generic;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;
using UnityEngine;

namespace Systems.WaveSystem
{
    public class WaveGenerator : MonoBehaviour
    {
        private bool initialized = false;

        public Wave GenerateWave(int waveNumber)
        {
            var packs = GeneratePacks(waveNumber);

            return new Wave(packs);
        }

        private List<WavePack> GeneratePacks(int wave)
        {
            var packs = new List<WavePack>();
            var waveData = WaveData.WavePacks[wave];
            var packCount = waveData.Key;
            var packRarity = waveData.Value;

            for (int i = 0; i < packCount; i++)
            {
                packs.Add(GeneratePack(packRarity));
            }

            return packs;
        }

        private WavePack GeneratePack(Rarities packRarity)
        {
            var packNpcRarities = WaveData.PackNpcs[packRarity];
            var wavePack = new WavePack();
            
            packNpcRarities.ForEach(npcRarity =>
            {
                wavePack.AddNpc(GenerateNpc(npcRarity));
            });

            return wavePack;
        }

        private Npc GenerateNpc(Rarities npcRarity)
        {
            var faction = GameManager.Instance.FactionManager.GetRandomEnemyFactionByStanding();
            var npcs = faction.GetNpcsByRarity(npcRarity);
            var npc = GetRandomNpc(npcs);

            var instantiatedNpc = Instantiate(npc);
            instantiatedNpc.gameObject.name = npc.Name;

            return instantiatedNpc;
        }

        public Npc GenerateSingleNpc(Npc npc)
        {
            var generatedNpc = Instantiate(npc);
            generatedNpc.Name = npc.name;
            generatedNpc.gameObject.layer = LayerMask.NameToLayer("Npcs");

            return generatedNpc;
        }

        private Npc GetRandomNpc(List<Npc> npcs)
        {
            return npcs[MathHelper.RandomInt(0, npcs.Count)];
        }
    }
}