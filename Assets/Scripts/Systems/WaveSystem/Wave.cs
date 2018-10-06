using System.Collections.Generic;
using Systems.NpcSystem;

namespace Systems.WaveSystem
{
    public class Wave
    {
        public List<WavePack> Packs { get; private set; }
        public int NpcCount { get; private set; }
        public int NpcSpawnCount { get; set; }
        public int PackCount { get; private set; }
        public int PackSpawnCount { get; set; }

        public int WaveNumber { get; set; }

        public List<Npc> SpawnedNpcs { get; private set; }

        public Wave(List<WavePack> packs)
        {
            this.Packs = packs;

            packs.ForEach(pack => { this.NpcCount += pack.GetNpcCount(); });
            this.PackCount = packs.Count;
            this.SpawnedNpcs = new List<Npc>();
        }
    }
}
