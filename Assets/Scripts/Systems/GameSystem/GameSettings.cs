using System.Collections.Generic;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.GameSystem
{
    static class GameSettings
    {
        #region Player Settings

        public static string Name = "Parcival";
        public static int StartingGold = 15;
        public static int StartingAmbassadors = 1;
        public static int StartingLives = 20;
        public static int TowerSlots = 8;

        #endregion end


        #region General Settings

        public static float SellTax = 0.5f;

        #endregion

        #region Colors
        public static Color MagicalCritColor = new Color(0.7f, 0.87f, 1.0f);
        public static Color AttackRangeIndicatorColor = new Color(0.71f, 0.52f, 0.19f, .70f);
        public static Color AuraRangeIndicatorColor = new Color(0.44f, 0.55f, 0.84f, .70f);
        #endregion

        #region Npc balancing settings

        public static float NpcXPBase = 0.0f;
        public static float NpcXPInc = 5.0f;

        public static float NpcGoldBase = 0.0f;
        public static float NpcGoldInc = 1.0f;

        public static float NpcGoldFactorExpBase = 2f;
        public static float NpcXpFactorExpBase = 4f;

        public static float BaseLineNpcAuraRange = 3f;

        public static Dictionary<Rarities, float> BaselineNpcHp = new Dictionary<Rarities, float>
        {
            {Rarities.None, 1.0f },
            {Rarities.Common, 30.0f},
            {Rarities.Uncommon, 60.0f},
            {Rarities.Rare, 90.0f},
            {Rarities.Legendary, 120.0f}
        };

        public static Dictionary<Rarities, float> BaselineNpcHpInc = new Dictionary<Rarities, float>
        {
            {Rarities.None, 1.0f },
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
        public static float BaseLineTowerAttackRange = 2.25f;

        public static float BaseLineTowerAuraRange = 1.5f;
        public static float BaseLineTowerAuraRangeInc = 0.05f;


        public static Dictionary<Rarities, float> BaselineTowerDmg = new Dictionary<Rarities, float>
        {
            {Rarities.None, 1.0f },
            {Rarities.Common, 25.0f},
            {Rarities.Uncommon, 50.0f},
            {Rarities.Rare, 75.0f},
            {Rarities.Legendary, 100.0f}
        };

        public static Dictionary<Rarities, float> BaselineTowerDmgInc = new Dictionary<Rarities, float>
        {
            {Rarities.None, 1.0f },
            {Rarities.Common, 0.04f},
            {Rarities.Uncommon, 0.04f},
            {Rarities.Rare, 0.04f},
            {Rarities.Legendary, 0.04f}
        };

        public static Dictionary<Rarities, int> BaselineTowerPrice = new Dictionary<Rarities, int>
        {
            {Rarities.None, 1 },
            {Rarities.Common, 5},
            {Rarities.Uncommon, 50},
            {Rarities.Rare, 500},
            {Rarities.Legendary, 5000}
        };
        #endregion

        public static bool Debug = false;
    }
}