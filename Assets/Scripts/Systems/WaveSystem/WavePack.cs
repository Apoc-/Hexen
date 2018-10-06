using System.Collections.Generic;
using Systems.NpcSystem;

namespace Systems.WaveSystem
{
    public class WavePack
    {
        private readonly List<Npc> npcs = new List<Npc>();

        public List<Npc> Npcs
        {
            get { return npcs; }
        }

        public void AddNpc(Npc npc)
        {
            this.npcs.Add(npc);
        }

        public void AddPack(WavePack pack)
        {
            this.npcs.AddRange(pack.npcs);
        }

        public int GetNpcCount()
        {
            return npcs.Count;
        }
    }
}