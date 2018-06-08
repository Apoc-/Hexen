using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using Hexen;
using Hexen.GameData;
using UnityEngine;
using UnityEngine.EventSystems;
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
            if (currentHeldTower != null && !EventSystem.current.IsPointerOverGameObject())
            {
                HandleTowerHolding();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CancelPickup();
            }
        }

        private void HandleTowerHolding()
        {
            Tile currentTile;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 objPosition;
            LayerMask mask = LayerMask.GetMask("Tiles");

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                var hitGameObject = hit.transform.gameObject;
                currentTile = hitGameObject.GetComponent<Tile>();

                if (currentTile != null)
                {
                    currentHeldTower.Tile = currentTile;
                    var towerIsPlaceable = TowerIsPlaceableOnTile(currentTile, currentHeldTower);

                    if (towerIsPlaceable)
                    {
                        SetTowerModelPlaceableColor();
                    }
                    else
                    {
                        SetTowerModelNotPlaceableColor();
                    }

                    if (currentTile.GetTopCenter() != currentHeldTower.gameObject.transform.position)
                    {
                        currentHeldTower.gameObject.transform.position = currentTile.GetTopCenter();

                        if (towerIsPlaceable)
                        {
                            GameManager.Instance.TowerSelectionManager.DisplayRangeIndicator(currentHeldTower);
                        }
                        else
                        {
                            GameManager.Instance.TowerSelectionManager.DestroyRangeIndicator();
                        }
                    }
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
        }

        public void LoadTowers()
        {
            var tdm = GameManager.Instance.TowerDataManager;
            tdm.InitializeTowers();
            availableTowers = tdm.GetAvailableTowers();
            Debug.Log("Loaded " + availableTowers.Count + " Towers.");
        }

        public Tower GetRandomTower()
        {
            var seed = (int) System.DateTime.Now.Ticks;
            Random rnd = new Random(seed);
            int r = rnd.Next(availableTowers.Count);

            return availableTowers[r];
        }

        public void GenerateStartingBuildableTowers(Player player)
        {
            for (int i = 0; i < player.TowerSlots; i++)
            {
                AddRandomBuildableTower(player);
            }
        }

        public void AddRandomBuildableTower(Player player)
        {
            var t = GetRandomTower();

            player.AddBuildableTower(t);
        }

        public void DebugAddBuildableTower()
        {
            var player = GameManager.Instance.Player;

            var t = GetRandomTower();

            player.AddBuildableTower(t);
        }

        public void PickUpTower(TowerBuildButtonBehaviour button)
        {
            CancelPickup();

            currentHeldTowerButton = button;
            currentHeldTowerButton.SetButtonActive();
            
            currentHeldTower = Instantiate(button.Tower);
            currentHeldTower.InitTowerModel();

            currentHeldTower.Name = button.Tower.Name;
            currentHeldTower.transform.parent = transform;
            
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
            if (currentHeldTowerButton != null)
            {
                currentHeldTowerButton.SetButtonInactive();
            }
            
            if (currentHeldTower != null)
            {
                Destroy(currentHeldTower.gameObject);
            }
        }

        public void SetTowerModelTransparency(float alpha)
        {
            var renderer = currentHeldTower.GetComponentsInChildren<Renderer>();

            foreach (var r in renderer)
            {
                if (r.material.HasProperty("color"))
                {
                    var color = r.material.color;
                    color.a = alpha;
                    r.material.color = color;
                }
            }
           
        }

        private void SetTowerModelColor(Color newColor)
        {
            var renderer = currentHeldTower.GetComponentsInChildren<MeshRenderer>();

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
