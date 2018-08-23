namespace Assets.Scripts.Systems.WaveSystem
{
    class WaveReward
    {
        public int Gold;
        public int Towers;
        public int Ambassadors;

        public WaveReward(int gold, int towers, int ambassadors)
        {
            Gold = gold;
            Towers = towers;
            Ambassadors = ambassadors;
        }
    }
}
