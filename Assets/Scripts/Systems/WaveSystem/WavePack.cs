using System.Collections.Generic;
using Systems.NpcSystem;

namespace Systems.WaveSystem
{
    public class WavePack
    {
        private readonly List<Npc> _npcs = new List<Npc>();

        public List<Npc> Npcs
        {
            get { return _npcs; }
        }

        public void AddNpc(Npc npc)
        {
            _npcs.Add(npc);
        }

        public void AddPack(WavePack pack)
        {
            _npcs.AddRange(pack._npcs);
        }

        public int GetNpcCount()
        {
            return _npcs.Count;
        }
    }
}