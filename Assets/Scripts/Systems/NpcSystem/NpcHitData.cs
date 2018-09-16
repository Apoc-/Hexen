using Assets.Scripts.Systems.TowerSystem;
using UnityEditor;

namespace Assets.Scripts.Definitions.Npcs
{
    public class NpcHitData
    {
        public float Dmg { get; set; }
        public Tower Source { get; set; }

        public NpcHitData(float dmg, Tower source)
        {
            this.Dmg = dmg;
            this.Source = source;
        }
    }
}