using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Tower = Assets.Scripts.Systems.TowerSystem.Tower;

namespace Assets.Scripts.Systems.GameSystem
{
    public class Player : MonoBehaviour
    {
        private int ambassadors = GameSettings.StartingAmbassadors;
        private int lives = GameSettings.StartingLives;

        private readonly List<Tower> buildableTowers = new List<Tower>();

        //events
        public delegate void GoldGainEvent(int amount);
        public event GoldGainEvent OnGainGold;

        public int Lives
        {
            get => lives;
            set
            {
                lives = value;
                if (lives <= 0)
                {
                    Die();
                }
            }
        }

        public int Gold { get; private set; } = GameSettings.StartingGold;

        public string Name { get; } = GameSettings.Name;

        public int TowerSlots { get; } = GameSettings.TowerSlots;

        private void Die()
        {
            GameManager.Instance.LoseGame();
        }


        public void IncreaseGold(int amount)
        {
            Gold += amount;

            OnGainGold?.Invoke(amount);
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

            if (Gold < cost) return false;

            DecreaseGold(cost);
            tower.Owner = this;

            return true;
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

            if (buildableTowers.Count >= TowerSlots)
            {
                var removedTower = buildableTowers[0];
                buildableTowers.Remove(removedTower);
                buildPanel.RemoveBuildButtonForTower(removedTower);
            }

            buildableTowers.Add(tower);
            buildPanel.AddBuildButtonForTower(tower);
        }

        public void RemoveBuildableTower(Tower tower)
        {
            buildableTowers.Remove(tower);
        }
    }
}
