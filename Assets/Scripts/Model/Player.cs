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
