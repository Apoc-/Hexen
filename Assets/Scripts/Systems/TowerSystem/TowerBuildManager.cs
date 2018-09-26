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
                GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.Standard);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && currentHeldTower != null && currentTile != null)
            {
                if (PlayerHasGold(currentHeldTower) && TileIsBuildslot(currentTile))
                {
                    PlaceTower(currentTile);
                    GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.Standard);
                }
            }
        }

        private void UpdateVisualEffects()
        {
            if (currentHeldTower == null || currentTile == null)
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
                GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.ReplaceTower);
            }

            if (towerHoldState != TowerHoldState.CanReplace)
            {
                GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.Standard);
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
            var rnd = MathHelper.RandomInt(0, availableTowers.Count);

            var towerPrefab = availableTowers[rnd];
            var tower = Instantiate(towerPrefab);

            tower.Name = towerPrefab.Name;
            tower.transform.parent = GameManager.Instance.TowerBuildManager.BuildableTowers.transform;

            tower.InitTower();

            return tower;
        }

        public void GenerateStartingBuildableTowers(Player player)
        {
            for (int i = 0; i < player.TowerSlots; i++)
            {
                AddRandomBuildableTowerForPlayer(player);
            }
        }

        public void AddRandomBuildableTowerForPlayer(Player player)
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

                currentHeldTowerButton.SetButtonInactive();
                GameManager.Instance.UIManager.BuildPanel.RemoveBuildButton(currentHeldTowerButton, true);
                currentHeldTowerButton = null;

                currentHeldTower.gameObject.transform.parent = PlacedTowers.transform;
                currentHeldTower.IsPlaced = true;
                currentHeldTower.Tile = tile;
                GameManager.Instance.Player.RemoveBuildableTower(currentHeldTower);
                currentHeldTower = null;
                
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
