﻿using System.Collections.Generic;
using UnityEngine;
using Tower = Systems.TowerSystem.Tower;

namespace Systems.GameSystem
{
    public class Player : MonoBehaviour
    {
        private int _ambassadors = GameSettings.StartingAmbassadors;
        private int _lives = GameSettings.StartingLives;

        private readonly List<Tower> _buildableTowers = new List<Tower>();

        //events
        public delegate void GoldGainEvent(int amount);
        public event GoldGainEvent OnGainGold;

        public int Lives
        {
            get => _lives;
            set
            {
                _lives = value;
                if (_lives <= 0)
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
            _ambassadors += amount;
        }

        public void DecreaseAmbassadors(int amount)
        {
            _ambassadors -= amount;
        }

        public int GetAmbassadors()
        {
            return _ambassadors;
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

            if (_buildableTowers.Count >= TowerSlots)
            {
                var removedTower = _buildableTowers[0];
                _buildableTowers.Remove(removedTower);
                buildPanel.RemoveBuildButtonForTower(removedTower);
            }

            _buildableTowers.Add(tower);
            buildPanel.AddBuildButtonForTower(tower);
        }

        public void RemoveBuildableTower(Tower tower)
        {
            _buildableTowers.Remove(tower);
        }
    }
}
