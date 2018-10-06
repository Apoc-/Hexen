using Systems.TowerSystem;

namespace Systems.NpcSystem
{
    public class NpcHitData
    {
        public float Dmg { get; set; }
        public Tower Source { get; set; }

        public NpcHitData(float dmg, Tower source)
        {
            Dmg = dmg;
            Source = source;
        }
    }
}