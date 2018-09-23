using System;
using System.Collections.Generic;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Tower = Assets.Scripts.Systems.TowerSystem.Tower;

namespace Assets.Scripts.Systems.GameSystem
{
    public class Player : MonoBehaviour
    {
        private readonly String playerName = GameSettings.Name;
        private int gold = GameSettings.StartingGold;
        private int ambassadors = GameSettings.StartingAmbassadors;
        private int lives = GameSettings.StartingLives;
        private int towerSlots = 8;

        private Queue<Tower> BuildableTowers = new Queue<Tower>();


        //events
        public delegate void GoldGainEvent(int amount);
        public event GoldGainEvent OnGainGold;

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

        public int Gold
        {
            get { return gold; }
            private set { gold = value; }
        }

        public string Name
        {
            get { return playerName; }
        }

        public int TowerSlots
        {
            get { return towerSlots; }
        }

        private void Die()
        {
            GameManager.Instance.LoseGame();
        }


        public void IncreaseGold(int amount)
        {
            Gold += amount;

            if (OnGainGold != null) OnGainGold(amount);
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

        public void AddRandomBuildableTowers(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                AddRandomBuildableTower();
            }   
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
            tower.Remove();

            IncreaseGold((int) (cost * GameSettings.SellTax));
        }

        public void AddBuildableTower(Tower tower)
        {
            var buildPanel = GameManager.Instance.UIManager.BuildPanel;

            if (BuildableTowers.Count >= TowerSlots)
            {
                var removedTower = BuildableTowers.Dequeue();
                buildPanel.RemoveBuildButtonForTower(removedTower);
            }

            BuildableTowers.Enqueue(tower);
            buildPanel.AddBuildButtonForTower(tower);
        }

        public void AddRandomBuildableTower()
        {
            GameManager.Instance.TowerBuildManager.AddRandomBuildableTowerForPlayer(this);
        }
    }
}
