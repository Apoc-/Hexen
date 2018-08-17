using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen.WaveSystem;

namespace Hexen.WaveSystem
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

            waves[0] = new Wave(typeof(Rat), size: 10, spawnInterval: 1.0f, goldReward: 10, towerReward: 1);
            waves[1] = new Wave(typeof(Rat), size: 30, spawnInterval: 0.25f, goldReward: 15, towerReward: 1);
            waves[2] = new Wave(typeof(Goblin), size: 10, spawnInterval: 1.0f, goldReward: 20, towerReward: 1);
            waves[3] = new Wave(typeof(Goblin), size: 30, spawnInterval: 0.25f, goldReward: 25, towerReward: 1);
            waves[4] = new Wave(typeof(Rat), size: 1, spawnInterval: 1.0f, goldReward: 30, towerReward: 1);
        }
    }
}
