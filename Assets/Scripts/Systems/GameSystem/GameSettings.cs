using System.Collections.Generic;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.GameSystem
{
    static class GameSettings
    {
        //balancing settings changed 29.08.2018
        

        #region Player Settings

        public static string Name = "Parcival";
        public static int StartingGold = 45;
        public static int StartingLives = 20;

        public static int StartingAmbassadors = 2;
        public static int AmbassadorsPerWave = 2;

        public static int StartingTowers = 2;
        public static int TowersPerWave = 1;
        public static int TowerSlots = 8;

        #endregion end


        #region General Settings

        public static float SellTax = 0.5f;

        #endregion

        #region Colors
        public static Color MagicalCritColor = new Color(0.7f, 0.87f, 1.0f);
        public static Color CritColor = new Color(0.61f, 0.11f, 0.11f, 1.0f);
        public static Color AttackRangeIndicatorColor = new Color(0.71f, 0.52f, 0.19f, .70f);
        public static Color AuraRangeIndicatorColor = new Color(0.44f, 0.55f, 0.84f, .70f);
        #endregion

        #region Npc balancing settings

        public static float NpcXPBase = 0.0f;
        public static float NpcXPInc = 5.0f;

        public static float NpcGoldBase = 0.0f;
        public static float NpcGoldInc = 0.5f;

        public static float NpcGoldFactorExpBase = 2f;
        public static float NpcXpFactorExpBase = 4f;

        public static float BaseLineNpcAuraRange = 3f;
        public static float BaseLineNpcMovementspeed = 0.75f;

        public static Dictionary<Rarities, float> BaselineNpcHp = new Dictionary<Rarities, float>
        {
            {Rarities.None, 1.0f },
            {Rarities.Common, 80.0f},
            {Rarities.Uncommon, 120.0f},
            {Rarities.Rare, 160.0f},
            {Rarities.Legendary, 200.0f}
        };

        public static Dictionary<Rarities, float> BaselineNpcHpInc = new Dictionary<Rarities, float>
        {
            {Rarities.None, 1.0f },
            {Rarities.Common, 0.2f},
            {Rarities.Uncommon, 0.2f},
            {Rarities.Rare, 0.2f},
            {Rarities.Legendary, 0.2f}
        };
        #endregion

        #region Tower balancing settings
        public static int TowerMaxLevel = 50;
        public static float LvlFact = 6.0f;
        public static float LvlExp = 3.0f;
        public static float LvlConst = 0.0f;

        public static float BaseLineTowerAttackSpeed = 1.2f;
        public static float BaseLineTowerAttackRange = 2.25f;

        public static float BaseLineTowerAuraRange = 1.5f;
        public static float BaseLineTowerAuraRangeInc = 0.05f;


        public static Dictionary<Rarities, float> BaselineTowerDmg = new Dictionary<Rarities, float>
        {
            {Rarities.None, 1 },
            {Rarities.Common, 10 },
            {Rarities.Uncommon, 45 },
            {Rarities.Rare, 190 },
            {Rarities.Legendary, 775 }
        };

        public static Dictionary<Rarities, float> BaselineTowerDmgInc = new Dictionary<Rarities, float>
        {
            {Rarities.None, 1.0f },
            {Rarities.Common, 0.04f },
            {Rarities.Uncommon, 0.04f },
            {Rarities.Rare, 0.04f },
            {Rarities.Legendary, 0.04f }
        };

        public static Dictionary<Rarities, int> BaselineTowerPrice = new Dictionary<Rarities, int>
        {
            {Rarities.None, 1 },
            {Rarities.Common, 25},
            {Rarities.Uncommon, 100},
            {Rarities.Rare, 400},
            {Rarities.Legendary, 1600}
        };
        #endregion

        public static bool Debug = false;
    }
}