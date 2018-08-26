using System;
using System.Collections.Generic;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Systems.GameSystem
{
    public class Player : MonoBehaviour
    {
        public String Name;
        public int Gold;
        private int ambassadors = GameSettings.StartingAmbassadors;

        [SerializeField] private int lives = 10;
        public int Lives
        {
            get { return lives; }
            set
            {
                lives = value;
                if (lives <= 0)
                {
                    Die();
                }
            }
        }

        private void Die()
        {
            GameManager.Instance.LoseGame();
        }

        public int TowerSlots = 8;

        private Queue<Tower> BuildableTowers = new Queue<Tower>();

        public void IncreaseGold(int amount)
        {
            Gold += amount;
        }

        public void DecreaseGold(int amount)
        {
            Gold -= amount;
        }

        public void IncreaseAmbassadors(int amount)
        {
            this.ambassadors += amount;
            GameManager.Instance.UIManager.FactionPanel.UpdateAmbassadorsLabel();
        }

        public void DecreaseAmbassadors(int amount)
        {
            this.ambassadors -= amount;
            GameManager.Instance.UIManager.FactionPanel.UpdateAmbassadorsLabel();
        }

        public int GetAmbassadors()
        {
            return this.ambassadors;
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

        public void SellTower(Tower tower)
        {
            var cost = tower.GoldCost;
            IncreaseGold((int) (cost * GameSettings.SellTax));

            tower.Remove();
        }

        public void AddBuildableTower(Tower t)
        {
            CheckBuildableTowersQueueLimit();
            BuildableTowers.Enqueue(t);
            GameManager.Instance.UIManager.BuildPanel.AddBuildButtonForTower(t);
        }

        public void AddRandomBuildableTower()
        {
            GameManager.Instance.TowerBuildManager.AddRandomBuildableTower(this);
        }

        private void CheckBuildableTowersQueueLimit()
        {
            while (BuildableTowers.Count >= TowerSlots)
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
