using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Systems.MapSystem
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
            SetTileMaterial(tile);
            return tile;
        }

        public static void SetTileMaterial(Tile tile)
        {
            var materials = TileMaterials[tile.TileType];
            var rndMaterial = materials[Rng.Next(materials.Count)];
            var material = Resources.Load<Material>("Materials/LandscapeMaterials/" + rndMaterial);

            tile.Material = material;
        }

        private static Tile GetBaseTile(char tileDatum, GameObject tilePrefab)
        {
            var tile = Instantiate(tilePrefab).GetComponent<Tile>();

            switch (tileDatum)
            {
                case '.':
                    tile.TileType = TileType.Path;
                    tile.Height = -0.2f;
                    break;
                case 'B':
                    tile.TileType = TileType.Buildslot;
                    tile.Height = 0.0f;
                    break;
                    
                case 'S':
                    tile.TileType = TileType.Start;
                    tile.Height = -0.15f;
                    break;

                case 'E':
                    tile.TileType = TileType.End;
                    tile.Height = -0.15f;
                    break;

                case 'm':
                    tile.TileType = TileType.Mountain;
                    tile.Height = 0.55f;
                    break;

                case 'M':
                    tile.TileType = TileType.MountainTop;
                    tile.Height = 1.35f;
                    break;

                case 's':
                    tile.TileType = TileType.Sand;
                    tile.Height = -0.45f;
                    break;

                case 'l':
                    tile.TileType = TileType.Lava;
                    tile.Height = -0.7f;
                    break;

                case 'L':
                    tile.TileType = TileType.VolcanoLava;
                    tile.Height = 1.25f;
                    break;

                case 'w':
                default:
                    tile.TileType = TileType.Water;
                    tile.Height = -0.7f;
                    break;
            }

            return tile;
        }
    }
}
