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
                    tile.Material = Resources.Load<Material>("Materials/PathMaterial");
                    tile.Height = 0.05f;
                    break;

                case 'E':
                    tile.TileType = TileType.End;
                    tile.Material = Resources.Load<Material>("Materials/PathMaterial");
                    tile.Height = 0.05f;
                    break;

                case 'x':
                default:
                    tile.TileType = TileType.Void;
                    tile.Material = Resources.Load<Material>("Materials/VoidMaterial");
                    tile.Height = -0.5f;
                    break;
            }

            return tile;
        }
    }
}
