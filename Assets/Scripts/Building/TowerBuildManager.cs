using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using Hexen;
using UnityEngine;
using Random = System.Random;

namespace Hexen
{
    class TowerBuildManager : MonoBehaviour
    {
        private List<Tower> availableTowers;
        private Tower currentHeldTower;

        private void Update()
        {
            HandleTowerHolding();
        }

        private void HandleTowerHolding()
        {
            if (currentHeldTower != null)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 objPosition;

                if (Physics.Raycast(ray, out hit))
                {
                    currentHeldTower.gameObject.transform.position = hit.transform.position;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PlaceTower();
                } else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    CancelPickup();
                }
            }
        }

        public void LoadTowers()
        {
            availableTowers = new List<Tower>(Resources.LoadAll<Tower>("Prefabs/Entities/Towers"));
            Debug.Log("Loaded " + availableTowers.Count + " Towers.");
        }

        public Tower GetRandomTower()
        {
            Random rnd = new Random();
            int r = rnd.Next(availableTowers.Count);

            return availableTowers[r];
        }

        public void AddRandomBuildableTower()
        {
            var t = GetRandomTower();

            GameManager.Instance.Player.AddBuildableTower(t);
        }

        public void PickUpTower(Tower tower)
        {
            var towerGo = Instantiate(tower);
            currentHeldTower = towerGo;
            
            towerGo.Name = tower.Name;
            towerGo.transform.parent = transform;
            
            SetTowerModelTransparency(0.25f);
        }

        private void PlaceTower()
        {
            SetTowerModelTransparency(1.0f);

            currentHeldTower.IsPlaced = true;
            currentHeldTower = null;
        }

        private void CancelPickup()
        {
            Destroy(currentHeldTower.gameObject);
        }

        public void SetTowerModelTransparency(float alpha)
        {
            var renderer = currentHeldTower.GetComponentInChildren<Renderer>();
            var color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
    }
}
