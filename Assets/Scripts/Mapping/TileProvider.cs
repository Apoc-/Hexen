using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Mapping;
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
                    break;
                case 'B':
                    tile.TileType = TileType.Buildslot;
                    tile.Material = Resources.Load<Material>("Materials/BuildslotMaterial");
                    break;
                    
                case 'S':
                    tile.TileType = TileType.Start;
                    tile.Material = Resources.Load<Material>("Materials/PathMaterial");
                    break;

                case 'E':
                    tile.TileType = TileType.End;
                    tile.Material = Resources.Load<Material>("Materials/PathMaterial");
                    break;

                case 'x':
                default:
                    tile.TileType = TileType.Void;
                    tile.Material = Resources.Load<Material>("Materials/VoidMaterial");
                    break;
            }

            return tile;
        }
    }
}
