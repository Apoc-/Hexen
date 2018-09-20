using System.Collections.Generic;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.MapSystem;
using Assets.Scripts.Systems.UiSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

namespace Assets.Scripts.Systems.TowerSystem
{
    class TowerBuildManager : MonoBehaviour
    {
        private enum TowerHoldState
        {
            CanPlace,
            CanReplace,
            NoGold,
            NoBuildSlot,
            None
        }


        private Tower currentHeldTower;
        private Tile currentTile;
        private TowerBuildButtonBehaviour currentHeldTowerButton;
        private TowerHoldState towerHoldState;

        public GameObject PlacedTowers;
        public GameObject BuildableTowers;

        private List<Tower> builtTowers = new List<Tower>();

        private void Update()
        {
            if (currentHeldTower != null && !EventSystem.current.IsPointerOverGameObject())
            {
                this.towerHoldState = ObtainTowerHoldState();

                UpdateVisualEffects();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CancelPickup();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && currentHeldTower != null && currentTile != null)
            {
                if (PlayerHasGold(currentHeldTower) && TileIsBuildslot(currentTile))
                {
                    PlaceTower(currentTile);
                }
            }
        }

        private void UpdateVisualEffects()
        {
            if (currentHeldTower == null)
            {
                GameManager.Instance.TowerSelectionManager.DestroyRangeIndicator();
                return;
            }

            currentHeldTower.gameObject.transform.position = currentTile.GetTopCenter();

            GameManager.Instance.TowerSelectionManager.UpdateRangeIndicator(currentHeldTower);

            if (towerHoldState == TowerHoldState.NoGold || towerHoldState == TowerHoldState.NoBuildSlot)
            {
                SetTowerModelNotPlaceableColor();
            }
            else
            {
                SetTowerModelPlaceableColor();
            }

            if (towerHoldState == TowerHoldState.CanReplace)
            {
                //show tower replace icon
            }
        }

        private TowerHoldState ObtainTowerHoldState()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Tiles");

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                var hitGameObject = hit.transform.gameObject;
                currentTile = hitGameObject.GetComponent<Tile>();

                if (currentTile == null || !TileIsBuildslot(currentTile))
                {
                   return TowerHoldState.NoBuildSlot;
                } 

                if (PlayerHasGold(currentHeldTower))
                {
                    if (currentTile.IsEmpty) return TowerHoldState.CanPlace;
                    if (currentTile.PlacedTower != null) return TowerHoldState.CanReplace;
                }
            }

            return TowerHoldState.NoBuildSlot;
        }

        public Tower GetRandomTower()
        {
            var availableTowers = GameManager.Instance.FactionManager.GetAvailableTowers();

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
            
            currentHeldTower = button.Tower;
            currentHeldTower.gameObject.SetActive(true);

            SetTowerModelTransparency(0.25f);
        }

        private void SellTower(Tower tower)
        {
            builtTowers.Remove(tower);
            GameManager.Instance.Player.SellTower(tower);
        }

        private void PlaceTower(Tile tile)
        {
            if (GameManager.Instance.Player.BuyTower(currentHeldTower))
            {
                if (tile.PlacedTower != null)
                {
                    currentHeldTower.GiveXP(tile.PlacedTower.Xp);
                    this.SellTower(tile.PlacedTower);
                }
                builtTowers.Add(currentHeldTower);

                SetTowerModelTransparency(1.0f);
                ResetTowerModelColor();
                tile.IsEmpty = false;
                tile.PlacedTower = currentHeldTower;

                currentHeldTower.gameObject.transform.parent = PlacedTowers.transform;
                currentHeldTower.IsPlaced = true;
                currentHeldTower = null;

                currentHeldTowerButton.SetButtonInactive();
                GameManager.Instance.UIManager.BuildPanel.RemoveBuildButton(currentHeldTowerButton);
                currentHeldTowerButton = null;

                GameManager.Instance.UIManager.InfoPopup.DisableTowerInfoPopup();
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
                currentHeldTower.gameObject.SetActive(false);
            }

            GameManager.Instance.UIManager.InfoPopup.DisableTowerInfoPopup();
            currentHeldTower = null;
            currentTile = null;
            towerHoldState = TowerHoldState.None;
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

        private bool PlayerHasGold(Tower tower)
        {
            return tower.GoldCost <= GameManager.Instance.Player.Gold;
        }
        private bool TileIsBuildslot(Tile tile)
        {
            return tile.TileType.Equals(TileType.Buildslot);
        }
    }
}
