using System.Collections.Generic;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Systems.WaveSystem
{
    public static class WaveData
    {
        public static int WaveCooldown = 5;
        public static float NpcSpawnInterval = 0.5f;

        public static Dictionary<int, KeyValuePair<int, Rarities>> WavePacks = new Dictionary<int, KeyValuePair<int, Rarities>>
        {
            { 1, new KeyValuePair<int, Rarities>(1, Rarities.Common)},
            { 2, new KeyValuePair<int, Rarities>(2, Rarities.Common)},
            { 3, new KeyValuePair<int, Rarities>(3, Rarities.Common)},
            { 4, new KeyValuePair<int, Rarities>(4, Rarities.Common)},
            { 5, new KeyValuePair<int, Rarities>(5, Rarities.Common)},

            { 6, new KeyValuePair<int, Rarities>(1, Rarities.Uncommon)},
            { 7, new KeyValuePair<int, Rarities>(2, Rarities.Uncommon)},
            { 8, new KeyValuePair<int, Rarities>(3, Rarities.Uncommon)},
            { 9, new KeyValuePair<int, Rarities>(4, Rarities.Uncommon)},
            { 10, new KeyValuePair<int, Rarities>(5, Rarities.Uncommon)},

            { 11, new KeyValuePair<int, Rarities>(1, Rarities.Rare)},
            { 12, new KeyValuePair<int, Rarities>(2, Rarities.Rare)},
            { 13, new KeyValuePair<int, Rarities>(3, Rarities.Rare)},
            { 14, new KeyValuePair<int, Rarities>(4, Rarities.Rare)},
            { 15, new KeyValuePair<int, Rarities>(5, Rarities.Rare)},

            { 16, new KeyValuePair<int, Rarities>(1, Rarities.Legendary)},
            { 17, new KeyValuePair<int, Rarities>(2, Rarities.Legendary)},
            { 18, new KeyValuePair<int, Rarities>(3, Rarities.Legendary)},
            { 19, new KeyValuePair<int, Rarities>(4, Rarities.Legendary)},
            { 20, new KeyValuePair<int, Rarities>(5, Rarities.Legendary)},
        };

        public static Dictionary<Rarities, List<Rarities>> PackNpcs = new Dictionary<Rarities, List<Rarities>>
        {
            { Rarities.Common, new List<Rarities>
                {
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Common
                }
            },
            { Rarities.Uncommon, new List<Rarities>
                {
                    Rarities.Uncommon,
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Uncommon,
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Uncommon,
                    Rarities.Common
                }
            },
            { Rarities.Rare, new List<Rarities>
                {
                    Rarities.Uncommon,
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Rare,
                    Rarities.Uncommon,
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Rare,
                    Rarities.Uncommon,
                    Rarities.Common
                }
            },
            { Rarities.Legendary, new List<Rarities>
                {
                    Rarities.Legendary,
                    Rarities.Uncommon,
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Rare,
                    Rarities.Uncommon,
                    Rarities.Common,
                    Rarities.Common,
                    Rarities.Rare,
                    Rarities.Uncommon,
                    Rarities.Common
                }
            },
        };
    }
}