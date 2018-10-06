using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Systems.MapSystem
{
    class TileProvider : MonoBehaviour
    {
        private static readonly Dictionary<TileType, List<String>> TileMaterials = new Dictionary<TileType, List<string>>
        {
            { TileType.Path, new List<string>{"Path"}},
            { TileType.Buildslot, new List<string>{"Buildslot"}},
            { TileType.Start, new List<string>{"StartEnd"}},
            { TileType.End, new List<string>{"StartEnd"}},
            { TileType.MountainTop, new List<string>{"MountainTop"}},
            { TileType.Mountain, new List<string>{"Mountain"}},
            { TileType.Sand, new List<string>{"Sand"}},
            { TileType.Lava, new List<string>{"Lava"}},
            { TileType.VolcanoLava, new List<string>{"Lava"}},
            //{ TileType.Water, new List<string>{"Water"}},
            { TileType.Water, new List<string>{"Void"}}
        };

        private static readonly Random Rng = new Random();

        public static Tile GetTile(char tileDatum, GameObject tilePrefab)
        {
            Tile tile = GetBaseTile(tileDatum, tilePrefab);
            if (tile.TileType == TileType.Void || tile.TileType == TileType.Water)
            {
                tile.gameObject.layer = LayerMask.NameToLayer("TransparentTiles");
            }

            SetTileMaterial(tile);
            return tile;
        }

        public static void SetTileMaterial(Tile tile)
        {
            var materials = TileMaterials[tile.TileType];
            var rndMaterial = materials[Rng.Next(materials.Count)];
            var material = Resources.Load<Material>("Materials/LandscapeMaterials/" + rndMaterial);
            var sleeveMaterial = Resources.Load<Material>("Materials/LandscapeMaterials/Sleeve");
            var voidMaterial = Resources.Load<Material>("Materials/LandscapeMaterials/Void");
            var renderer = tile.GetComponent<MeshRenderer>();
            var mats = renderer.materials;

            tile.Material = material;

            if (tile.TileType == TileType.Void || tile.TileType == TileType.Water)
            {
                mats[1] = voidMaterial;
            }
            else
            {
                mats[1] = sleeveMaterial;
            }

            renderer.materials = mats;
        }

        private static Tile GetBaseTile(char tileDatum, GameObject tilePrefab)
        {
            var tile = Instantiate(tilePrefab).GetComponent<Tile>();

            switch (tileDatum)
            {
                case '.':
                    tile.TileType = TileType.Path;
                    tile.DeltaHeight = -0.2f;
                    break;
                case 'B':
                    tile.TileType = TileType.Buildslot;
                    tile.DeltaHeight = 0.0f;
                    break;
                    
                case 'S':
                    tile.TileType = TileType.Start;
                    tile.DeltaHeight = -0.15f;
                    break;

                case 'E':
                    tile.TileType = TileType.End;
                    tile.DeltaHeight = -0.15f;
                    break;

                case 'm':
                    tile.TileType = TileType.Mountain;
                    tile.DeltaHeight = 0.55f;
                    break;

                case 'M':
                    tile.TileType = TileType.MountainTop;
                    tile.DeltaHeight = 1.35f;
                    break;

                case 's':
                    tile.TileType = TileType.Sand;
                    tile.DeltaHeight = -0.45f;
                    break;

                case 'l':
                    tile.TileType = TileType.Lava;
                    tile.DeltaHeight = -0.7f;
                    break;

                case 'L':
                    tile.TileType = TileType.VolcanoLava;
                    tile.DeltaHeight = 1.25f;
                    break;

                case 'w':
                default:
                    tile.TileType = TileType.Water;
                    tile.DeltaHeight = -0.7f;
                    break;
            }

            return tile;
        }
    }
}
