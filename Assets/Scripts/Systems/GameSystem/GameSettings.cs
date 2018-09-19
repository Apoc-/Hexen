using System.Collections.Generic;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Systems.GameSystem
{
    static class GameSettings
    {
        #region General settings

        public static float SellTax = 0.5f;
        public static int StartingAmbassadors = 1;

        #endregion

        #region Npc balancing settings

        public static float NpcXPBase = 0.0f;
        public static float NpcXPInc = 2.0f;

        public static float NpcGoldBase = 0.0f;
        public static float NpcGoldInc = 1.0f;

        public static float NpcGoldFactorExpBase = 4f;
        public static float NpcXpFactorExpBase = 10f;

        public static Dictionary<Rarities, float> BaselineNpcHp = new Dictionary<Rarities, float>
        {
            {Rarities.Common, 30.0f},
            {Rarities.Uncommon, 60.0f},
            {Rarities.Rare, 90.0f},
            {Rarities.Legendary, 120.0f}
        };

        public static Dictionary<Rarities, float> BaselineNpcHpInc = new Dictionary<Rarities, float>
        {
            {Rarities.Common, 0.25f},
            {Rarities.Uncommon, 0.25f},
            {Rarities.Rare, 0.25f},
            {Rarities.Legendary, 0.25f}
        };
        #endregion

        #region Tower balancing settings
        public static int TowerMaxLevel = 50;
        public static float LvlFact = 6.0f;
        public static float LvlExp = 3.0f;
        public static float LvlConst = 0.0f;

        public static float BaseLineTowerAttackSpeed = 1.5f;
        public static float BaseLineTowerAttackRange = 1.5f;

        public static float BaseLineTowerAuraRange = 1.5f;
        public static float BaseLineTowerAuraRangeInc = 0.05f;


        public static Dictionary<Rarities, float> BaselineTowerDmg = new Dictionary<Rarities, float>
        {
            {Rarities.Common, 25.0f},
            {Rarities.Uncommon, 50.0f},
            {Rarities.Rare, 75.0f},
            {Rarities.Legendary, 100.0f}
        };

        public static Dictionary<Rarities, float> BaselineTowerDmgInc = new Dictionary<Rarities, float>
        {
            {Rarities.Common, 0.04f},
            {Rarities.Uncommon, 0.04f},
            {Rarities.Rare, 0.04f},
            {Rarities.Legendary, 0.04f}
        };
        #endregion

    }
}