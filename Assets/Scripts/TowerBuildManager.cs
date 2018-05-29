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
        private TowerBuildButtonBehaviour currentHeldTowerButton;

        private void Update()
        {
            if (currentHeldTower != null)
            {
                HandleTowerHolding();
            }
        }

        private void HandleTowerHolding()
        {
            Tile currentTile;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 objPosition;

            if (Physics.Raycast(ray, out hit))
            {
                var hitGameObject = hit.transform.gameObject;
                currentTile = hitGameObject.GetComponent<Tile>();

                if (currentTile != null)
                {
                    if (TowerIsPlaceableOnTile(currentTile, currentHeldTower))
                    {
                        SetTowerModelPlaceableColor();
                    }
                    else
                    {
                        SetTowerModelNotPlaceableColor();
                    }

                    currentHeldTower.gameObject.transform.position = currentTile.GetTopCenter();
                }

                HandleTowerHoldingInput(currentTile);
            }
        }

        private void HandleTowerHoldingInput(Tile currentTile)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && TowerIsPlaceableOnTile(currentTile, currentHeldTower))
            {
                PlaceTower(currentTile);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CancelPickup();
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

        public void PickUpTower(Tower tower, TowerBuildButtonBehaviour button)
        {
            currentHeldTowerButton = button;
            currentHeldTowerButton.SetButtonActive();

            var towerGo = Instantiate(tower);
            currentHeldTower = towerGo;
            
            towerGo.Name = tower.Name;
            towerGo.transform.parent = transform;
            
            SetTowerModelTransparency(0.25f);
        }

        private void PlaceTower(Tile tile)
        {
            if (GameManager.Instance.Player.BuyTower(currentHeldTower))
            {
                SetTowerModelTransparency(1.0f);
                ResetTowerModelColor();
                tile.IsEmpty = false;
                currentHeldTower.IsPlaced = true;
                currentHeldTower = null;
                currentHeldTowerButton.SetButtonInactive();

                GameManager.Instance.UIManager.BuildPanel.RemoveBuildButton(currentHeldTowerButton);
                currentHeldTowerButton = null;
            }
            else
            {
                CancelPickup();
            }
        }

        private void CancelPickup()
        {
            Destroy(currentHeldTower.gameObject);
            currentHeldTowerButton.SetButtonInactive();
        }

        public void SetTowerModelTransparency(float alpha)
        {
            var renderer = currentHeldTower.GetComponentsInChildren<Renderer>();

            foreach (var r in renderer)
            {
                var color = r.material.color;
                color.a = alpha;
                r.material.color = color;
            }
           
        }

        private void SetTowerModelColor(Color newColor)
        {
            var renderer = currentHeldTower.GetComponentsInChildren<Renderer>();

            foreach (var r in renderer)
            {
                newColor.a = r.material.color.a;
                r.material.color = newColor;
            }
        }

        private void SetTowerModelNotPlaceableColor()
        {
            SetTowerModelColor(Color.red);
        }

        private void SetTowerModelPlaceableColor()
        {
            SetTowerModelColor(Color.green);
        }

        private void ResetTowerModelColor()
        {
            SetTowerModelColor(Color.white);
        }

        private bool TowerIsPlaceableOnTile(Tile tile, Tower tower)
        {
            return tile.IsEmpty && tile.TileType.Equals(TileType.Buildslot) && tower.GoldCost <= GameManager.Instance.Player.Gold;
        }
    }
}
