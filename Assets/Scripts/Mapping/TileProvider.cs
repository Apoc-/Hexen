using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;
using UnityEngine;

namespace Hexen
{
    class TileProvider : MonoBehaviour
    {
        public static Tile getTile(char tileDatum, GameObject tilePrefab)
        {
            var tile = Instantiate(tilePrefab).GetComponent<Tile>();

            switch (tileDatum)
            {
                case '.':
                    tile.TileType = TileType.Path;
                    tile.Material = Resources.Load<Material>("Materials/PathMaterial");
                    tile.Height = 0;
                    break;
                case 'B':
                    tile.TileType = TileType.Buildslot;
                    tile.Material = Resources.Load<Material>("Materials/BuildslotMaterial");
                    tile.Height = 0.2f;
                    break;
                    
                case 'S':
                    tile.TileType = TileType.Start;
                    tile.Material = Resources.Load<Material>("Materials/StartEndMaterial");
                    tile.Height = 0.05f;
                    break;

                case 'E':
                    tile.TileType = TileType.End;
                    tile.Material = Resources.Load<Material>("Materials/StartEndMaterial");
                    tile.Height = 0.05f;
                    break;

                case 'm':
                    tile.TileType = TileType.Mountain;
                    tile.Material = Resources.Load<Material>("Materials/MountainMaterial");
                    tile.Height = 0.75f;
                    break;

                case 'M':
                    tile.TileType = TileType.MountainTop;
                    tile.Material = Resources.Load<Material>("Materials/MountainTopMaterial");
                    tile.Height = 1.5f;
                    break;

                case 's':
                    tile.TileType = TileType.Sand;
                    tile.Material = Resources.Load<Material>("Materials/SandMaterial");
                    tile.Height = -0.25f;
                    break;

                case 'l':
                    tile.TileType = TileType.Lava;
                    tile.Material = Resources.Load<Material>("Materials/LavaMaterial");
                    tile.Height = -0.5f;
                    break;

                case 'L':
                    tile.TileType = TileType.VolcanoLava;
                    tile.Material = Resources.Load<Material>("Materials/LavaMaterial");
                    tile.Height = 1.45f;
                    break;

                case 'w':
                default:
                    tile.TileType = TileType.Water;
                    tile.Material = Resources.Load<Material>("Materials/WaterMaterial");
                    tile.Height = -0.5f;
                    break;
            }

            return tile;
        }
    }
}
