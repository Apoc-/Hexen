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

        private Tower CurrentHeldTower => GameManager.Instance.TowerSelectionManager.CurrentSelectedTower;
        private bool isBuilding = false;
        private Tile currentTile;
        private TowerBuildButtonBehaviour currentHeldTowerButton;
        private TowerHoldState towerHoldState;

        public GameObject PlacedTowers;
        public GameObject BuildableTowers;

        private List<Tower> builtTowers = new List<Tower>();

        private void Update()
        {
            if (CurrentHeldTower == null) isBuilding = false;

            if (!isBuilding) return;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                this.towerHoldState = ObtainTowerHoldState();

                if (currentTile != null)
                {
                    UpdateVisualEffects();
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CancelBuilding();
                GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.Standard);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) 
                && currentTile != null 
                && !EventSystem.current.IsPointerOverGameObject())
            {
                if (PlayerHasGold(CurrentHeldTower) && TileIsBuildslot(currentTile))
                {
                    BuildTower(currentTile);
                    GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.Standard);
                    GameManager.Instance.TowerSelectionManager.DeselectTower();
                }
            }
        }

        private void UpdateVisualEffects()
        {
            CurrentHeldTower.gameObject.transform.position = currentTile.GetTopCenter();

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

                if (PlayerHasGold(CurrentHeldTower))
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


        public void StartBuilding(TowerBuildButtonBehaviour button)
        {
            CancelBuilding();

            isBuilding = true;
            GameManager.Instance.TowerSelectionManager.SelectTower(button.Tower);

            currentHeldTowerButton = button;
            currentHeldTowerButton.SetButtonActive();
            
            CurrentHeldTower.gameObject.SetActive(true);

            SetTowerModelTransparency(0.25f);
        }

        private void SellTower(Tower tower)
        {
            builtTowers.Remove(tower);
            GameManager.Instance.Player.SellTower(tower);
        }

        private void BuildTower(Tile tile)
        {
            if (GameManager.Instance.Player.BuyTower(CurrentHeldTower))
            {
                if (tile.PlacedTower != null)
                {
                    CurrentHeldTower.GiveXP(tile.PlacedTower.Xp);
                    this.SellTower(tile.PlacedTower);
                }

                builtTowers.Add(CurrentHeldTower);

                SetTowerModelTransparency(1.0f);
                ResetTowerModelColor();
                tile.IsEmpty = false;
                tile.PlacedTower = CurrentHeldTower;

                currentHeldTowerButton.SetButtonInactive();
                GameManager.Instance.UIManager.BuildPanel.RemoveBuildButton(currentHeldTowerButton, true);
                currentHeldTowerButton = null;

                CurrentHeldTower.gameObject.transform.parent = PlacedTowers.transform;
                CurrentHeldTower.IsPlaced = true;
                CurrentHeldTower.Tile = tile;
                CurrentHeldTower.gameObject.SetActive(true);

                GameManager.Instance.Player.RemoveBuildableTower(CurrentHeldTower);
                isBuilding = false;
            }
            else
            {
                CancelBuilding();
            }
        }

        private void CancelBuilding()
        {
            if (currentHeldTowerButton != null)
            {
                currentHeldTowerButton.SetButtonInactive();
            }
            
            if (CurrentHeldTower != null && !CurrentHeldTower.IsPlaced)
            {
                CurrentHeldTower.gameObject.SetActive(false);
            }

            GameManager.Instance.UIManager.InfoPopup.DisableTowerInfoPopup();
            GameManager.Instance.TowerSelectionManager.DeselectTower();

            currentHeldTowerButton = null;
            currentTile = null;
            towerHoldState = TowerHoldState.None;
            isBuilding = false;
        }

        public void SetTowerModelTransparency(float alpha)
        {
            var renderer = CurrentHeldTower.GetComponentsInChildren<Renderer>();

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
            var renderer = CurrentHeldTower.GetComponentsInChildren<MeshRenderer>();

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
        public void DebugAddBuildableTower()
        {
            var player = GameManager.Instance.Player;

            var t = GetRandomTower();

            player.AddBuildableTower(t);
        }
    }
}
