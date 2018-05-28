using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen
{
    public class Player : Entity
    {
        public int Gold;
        public int MaxBuildableTowers = 4;

        private Queue<Tower> BuildableTowers = new Queue<Tower>();

        public void IncreaseGold(int amount)
        {
            Gold += amount;
        }
        public void DecreaseGold(int amount)
        {
            Gold -= amount;
        }

        public bool BuyTower(Tower tower)
        {
            var cost = tower.GoldCost;
            var success = false;

            if (Gold >= cost)
            {
                DecreaseGold(cost);
                tower.Owner = this;
                success = true;
            }

            return success;
        }

        public void AddBuildableTower(Tower t)
        {
            CheckBuildableTowersQueueLimit();
            BuildableTowers.Enqueue(t);
            UIManager.Instance.GetBuildPanelBehaviour().AddBuildButtonForTower(t);
        }

        private void CheckBuildableTowersQueueLimit()
        {
            while (BuildableTowers.Count >= MaxBuildableTowers)
            {
                BuildableTowers.Dequeue();
            }
        }

        public Queue<Tower> GetBuildableTowers()
        {
            return BuildableTowers;
        }
    }
}
