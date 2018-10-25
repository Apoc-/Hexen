using System.Collections.Generic;
using Systems.FactionSystem;
using Systems.GameSystem;
using Systems.MapSystem;
using Systems.UiSystem;
using Systems.UiSystem.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.TowerSystem
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

        public bool IsBuilding { get; private set; }

        private Tile _currentTile;
        private TowerBuildButton _currentHeldTowerButton;
        private TowerHoldState _towerHoldState;

        public GameObject PlacedTowersContainer;
        public GameObject BuildableTowersContainer;

        public List<Tower> BuiltTowers { get; } = new List<Tower>();

        private void Update()
        {
            if (CurrentHeldTower == null) IsBuilding = false;

            if (!IsBuilding) return;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _towerHoldState = ObtainTowerHoldState();

                if (_currentTile != null)
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
                && _currentTile != null 
                && !EventSystem.current.IsPointerOverGameObject())
            {
                if (PlayerHasGold(CurrentHeldTower) && TileIsBuildslot(_currentTile))
                {
                    BuildTower(_currentTile);
                    GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.Standard);
                    GameManager.Instance.TowerSelectionManager.DeselectTower();

                }
            }
        }

        private void UpdateVisualEffects()
        {
            CurrentHeldTower.gameObject.transform.position = _currentTile.GetTopCenter();

            if (_towerHoldState == TowerHoldState.NoGold || _towerHoldState == TowerHoldState.NoBuildSlot)
            {
                SetTowerModelNotPlaceableColor();
            }
            else
            {
                SetTowerModelPlaceableColor();
            }

            if (_towerHoldState == TowerHoldState.CanReplace)
            {
                GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.ReplaceTower);
            }

            if (_towerHoldState != TowerHoldState.CanReplace)
            {
                GameManager.Instance.UIManager.CursorHandler.SwitchCursor(Cursors.Standard);
            }
        }

        private TowerHoldState ObtainTowerHoldState()
        {
            if (Camera.main == null) return TowerHoldState.NoBuildSlot;
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Tiles");

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                var hitGameObject = hit.transform.gameObject;
                _currentTile = hitGameObject.GetComponent<Tile>();

                if (_currentTile == null || !TileIsBuildslot(_currentTile))
                {
                    return TowerHoldState.NoBuildSlot;
                } 

                if (PlayerHasGold(CurrentHeldTower))
                {
                    if (_currentTile.IsEmpty) return TowerHoldState.CanPlace;
                    if (_currentTile.PlacedTower != null) return TowerHoldState.CanReplace;
                }
            }

            return TowerHoldState.NoBuildSlot;
        }

        public Tower GetRandomTowerByStanding()
        {
            var faction = GameManager.Instance.FactionManager.GetRandomAlliedFactionByStanding();

            //roll rarity depending on current game progress
            var rarity = GetRandomRarityByWaveAndFaction(faction);

            //roll tower
            var towers = faction.GetTowersByRarity(rarity);
            var rnd = MathHelper.RandomInt(0, towers.Count);
            var towerPrefab = towers[rnd];

            //instantiate
            var tower = Instantiate(towerPrefab);

            tower.Name = towerPrefab.Name;
            tower.transform.parent = GameManager.Instance.TowerBuildManager.BuildableTowersContainer.transform;

            tower.InitTower();

            return tower;
        }

        private Rarities GetRandomRarityByWaveAndFaction(Faction faction)
        {
            var currentWave = GameManager.Instance.WaveSpawner.CurrentWaveCount;
            var maxWaves = GameManager.Instance.WaveSpawner.NumberOfWaves;
            var bound = maxWaves / 5.0f * 0.5f;
            var standingFactor = (float) faction.GetStanding() / (2 * currentWave);

            var r = MathHelper.RandomFloat() * currentWave * standingFactor;

            if (r > bound * 6) return Rarities.Legendary;
            if (r > bound * 4) return Rarities.Rare;
            if (r > bound * 2) return Rarities.Uncommon;

            return Rarities.Common;
        }

        public void GenerateStartingBuildableTowers(Player player)
        {
            for (var i = 0; i < GameSettings.StartingTowers; i++)
            {
                AddRandomBuildableTower();
            }
        }

        public void AddRandomBuildableTower()
        {
            var t = GetRandomTowerByStanding();

            GameManager.Instance.Player.AddBuildableTower(t);
        }


        public void StartBuilding(TowerBuildButton button)
        {
            CancelBuilding();

            IsBuilding = true;
            GameManager.Instance.TowerSelectionManager.SelectTower(button.Tower);

            _currentHeldTowerButton = button;
            _currentHeldTowerButton.SetButtonActive();
            
            CurrentHeldTower.gameObject.SetActive(true);

            SetTowerModelTransparency(0.25f);
        }

        private void SellTower(Tower tower)
        {
            BuiltTowers.Remove(tower);
            GameManager.Instance.Player.SellTower(tower);
        }

        private void BuildTower(Tile tile)
        {
            if (GameManager.Instance.Player.BuyTower(CurrentHeldTower))
            {
                if (tile.PlacedTower != null)
                {
                    CurrentHeldTower.GiveXP(tile.PlacedTower.Xp);
                    SellTower(tile.PlacedTower);
                }

                BuiltTowers.Add(CurrentHeldTower);

                SetTowerModelTransparency(1.0f);
                ResetTowerModelColor();
                tile.IsEmpty = false;
                tile.PlacedTower = CurrentHeldTower;

                _currentHeldTowerButton.SetButtonInactive();
                GameManager.Instance.UIManager.BuildPanel.RemoveBuildButton(_currentHeldTowerButton, true);
                _currentHeldTowerButton = null;

                CurrentHeldTower.gameObject.transform.parent = PlacedTowersContainer.transform;
                CurrentHeldTower.IsPlaced = true;
                CurrentHeldTower.CurrentTile = tile;
                CurrentHeldTower.gameObject.SetActive(true);

                GameManager.Instance.Player.RemoveBuildableTower(CurrentHeldTower);
                IsBuilding = false;
            }
            else
            {
                CancelBuilding();
            }
        }

        private void CancelBuilding()
        {
            if (_currentHeldTowerButton != null)
            {
                _currentHeldTowerButton.SetButtonInactive();
            }
            
            if (CurrentHeldTower != null && !CurrentHeldTower.IsPlaced)
            {
                CurrentHeldTower.gameObject.SetActive(false);
            }

            GameManager.Instance.UIManager.TowerInfoPanel.DisableTowerInfoPopup();
            GameManager.Instance.TowerSelectionManager.DeselectTower();

            _currentHeldTowerButton = null;
            _currentTile = null;
            _towerHoldState = TowerHoldState.None;
            IsBuilding = false;
        }

        public void SetTowerModelTransparency(float alpha)
        {
            var towerRenderer = CurrentHeldTower.GetComponentsInChildren<Renderer>();

            foreach (var r in towerRenderer)
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
            var towerRenderer = CurrentHeldTower.GetComponentsInChildren<MeshRenderer>();

            foreach (var r in towerRenderer)
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

            var t = GetRandomTowerByStanding();

            player.AddBuildableTower(t);

            //RarityTest();
        }

        /*private void RarityTest()
        {
            for (int i = 0; i < 2; i++)
            {
                Dictionary<Rarities, int> count = new Dictionary<Rarities, int>
                {
                    { Rarities.Common, 0 },
                    { Rarities.Uncommon, 0 },
                    { Rarities.Rare, 0 },
                    { Rarities.Legendary, 0 },
                };

                for (int j = 0; j < 1000; j++)
                {
                    var t = GetRandomTowerByStanding();
                    count[t.Rarity] += 1;
                    Destroy(t);
                }

                Debug.Log("C " + count[Rarities.Common]);
                Debug.Log("U " + count[Rarities.Uncommon]);
                Debug.Log("R " + count[Rarities.Rare]);
                Debug.Log("L " + count[Rarities.Legendary]);
                Debug.Log("-------------------------------");
            }
        }*/
    }
}
