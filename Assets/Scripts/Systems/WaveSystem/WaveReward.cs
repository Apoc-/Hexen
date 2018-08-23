using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hexen.WaveSystem
{
    class WaveReward
    {
        public int Gold;
        public int Towers;

        public WaveReward(int gold, int towers)
        {
            Gold = gold;
            Towers = towers;
        }
    }
}
