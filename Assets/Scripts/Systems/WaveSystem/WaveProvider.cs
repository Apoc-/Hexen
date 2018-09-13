using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;

namespace Assets.Scripts.Systems.WaveSystem
{
    static class WaveProvider
    {
        private static Dictionary<int, Wave> waves;

        private static Dictionary<int, Wave> Waves
        {
            get
            {
                if (waves == null)
                {
                    InitializeWaves();
                }

                return waves;
            }
        }

        public static int WaveCount
        {
            get { return Waves.Count; }
        }

        public static Wave ProvideWaveByID(int id)
        {
            return Waves[id];
        }

        private static void InitializeWaves()
        {
            waves = new Dictionary<int, Wave>();

            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
            AddWave(new Wave(typeof(Rat), size: 10, spawnInterval: 0.5f, goldReward: 10, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 20, spawnInterval: 0.5f, goldReward: 5, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 2, towerReward: 1, ambassadorReward: 0));
            AddWave(new Wave(typeof(RatAbomination), size: 1, spawnInterval: 1.0f, goldReward: 100, towerReward: 1, ambassadorReward: 1));
        }

        private static void AddWave(Wave wave)
        {
            waves[waves.Count] = wave;
        }
    }
}
