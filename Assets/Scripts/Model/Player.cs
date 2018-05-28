using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen
{
    public class Player : Entity
    {
        public int Gold;

        public List<Tower> BuildableTowers { get; set; }

        public void IncreaseGold(int amount)
        {
            Gold += amount;
        }
    }
}
