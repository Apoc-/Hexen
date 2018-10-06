using System;
using System.Collections.Generic;
using System.Linq;
using Systems.GameSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Systems.MapSystem
{
    public class MapManager : MonoBehaviour
    {
        [FormerlySerializedAs("currentMap")] [SerializeField] private TextAsset _currentMap;

        private float _tileSpacing = 0f;
        private float _outerRadius = 0.5f;
        private float _innerRadius = 0.5f * Mathf.Sqrt(3) / 2;
        public float BaseHeight { get; } = -2f;

        private float _randomHeightOffset = 0.0f;

        private List<List<Tile>> _tiles = new List<List<Tile>>();
        private List<Tile> _path = new List<Tile>();
        public Tile StartTile;
        public Tile EndTile;

        public float MapWidth { get; set; }
        public float MapHeight { get; set; }
        public float LeftBound { get; set; }
        public float UpperBound { get; set; }

        protected MapManager()
        {

        }

        // Use this for initialization
        void Start()
        {
            ParseMapFile(_currentMap.text);

            DynamicGI.UpdateEnvironment();
        }

        private void ParseMapFile(string map)
        {
            GameObject parent = gameObject;
            float spacing = _tileSpacing;
            
            StartTile = null;
            EndTile = null;

            GameObject prefab = Resources.Load<GameObject>("Prefabs/Terrain/Tile_top");
            List<string> lines = map.Split('\n').ToList();

            for (int lineIdx = 0; lineIdx < lines.Count; lineIdx++)
            {
                string line = lines[lineIdx].Trim();
                if (line == string.Empty) continue;
                List<string> tileData = line.Split(' ').ToList();

                List<Tile> tileRow = new List<Tile>();
                _tiles.Add(tileRow);

                for (int rowIdx = 0; rowIdx < tileData.Count; rowIdx++)
                {
                    char tileDatum = char.Parse(tileData[rowIdx]);

                    Tile tile = TileProvider.GetTile(tileDatum, prefab);

                    tile.transform.SetParent(parent.transform);

                    Vector3 newPosition = new Vector3();
                    newPosition.x = (_innerRadius * 2 + spacing) * rowIdx;
                    newPosition.z = -(_outerRadius * 2 + spacing) * (3f / 4f) * lineIdx;

                    var seed = (int)DateTime.Now.Ticks;
                    var rnd = new Random(seed);
                    float r = rnd.Next((int)(_randomHeightOffset*100)) / 100.0f;

                    newPosition.y = BaseHeight + r + tile.DeltaHeight;
                    

                    if (lineIdx % 2 == 0)
                    {
                        newPosition.x -= (_innerRadius * 2 + spacing) / 2;
                    }
                    tile.transform.SetPositionAndRotation(newPosition, tile.transform.rotation);

                    tile.GetComponent<Renderer>().material = tile.Material;
                    tileRow.Add(tile);

                    if (tile.TileType == TileType.Start)
                    {
                        StartTile = tile;
                    }
                    if (tile.TileType == TileType.End)
                    {
                        EndTile = tile;
                    }
                }
            }

            CreateNavMesh();
            CalculateMapExtents();
        }

        private void CalculateMapExtents()
        {
            MapWidth = _tiles[0].Count * _innerRadius * 2;
            MapHeight = _tiles.Count * _outerRadius * 2;
            LeftBound = _tiles[0][0].transform.position.x - _innerRadius;
            UpperBound = _tiles[0][0].transform.position.z - _outerRadius;
        }

        private void CreateNavMesh()
        {
            if (StartTile == null || EndTile == null)
            {
                throw new Exception("Map has no defined start and/or end tile.");
            }

            StartTile.NumberInPath = 0;
            _path.Add(StartTile);
            while (true)
            {
                var neighbors = GetTileNeighbors(_path.Last());
                var filteredNeighbors = neighbors.Where(t =>
                    t.TileType == TileType.Path
                    && !_path.Contains(t)).ToList();
                var nextTile = filteredNeighbors.FirstOrDefault();

                if (nextTile != null)
                {
                    nextTile.NumberInPath = _path.Last().NumberInPath + 1;
                    _path.Add(nextTile);
                }
                else if (neighbors.Contains(EndTile))
                {
                    EndTile.NumberInPath = _path.Last().NumberInPath + 1;
                    _path.Add(EndTile);
                    break;
                }
                else
                {
                    throw new Exception("No path found from Start to End Tile.");
                }
            }

            if (GameSettings.Debug)
            {
                for (int i = 0; i < _path.Count-1; i++)
                {
                    var t1 = _path[i];
                    var t2 = _path[i + 1];

                    Debug.DrawLine(t1.GetTopCenter(), t2.GetTopCenter(), Color.white, 9999);
                }
            }
        }

        private List<Tile> GetTileNeighbors(Tile tile)
        {
            List<Tile> neighbors = new List<Tile>();
            int lineIdx;
            int rowIdx = 0;
            for (lineIdx = 0; lineIdx < _tiles.Count; lineIdx++)
            {
                for (rowIdx = 0; rowIdx < _tiles[lineIdx].Count; rowIdx++)
                {
                    if (_tiles[lineIdx][rowIdx] == tile)
                    {
                        goto doublebreak;
                    }
                }
            }

        doublebreak:

            List<Tile> above = new List<Tile>();
            List<Tile> next = new List<Tile>(_tiles[lineIdx]);
            List<Tile> below = new List<Tile>();
            if (lineIdx > 0)
            {
                above.AddRange(_tiles[lineIdx - 1]);
            }
            if (lineIdx < _tiles.Count - 1)
            {
                below.AddRange(_tiles[lineIdx + 1]);
            }

            // if (lineIdx % 2 == 0)
            // offset to the left
            int aboveOrBelowOffset = lineIdx % 2 == 0
                ? -1
                : 0;

            neighbors.Add(GetElementOrDefault(next, rowIdx - 1));
            neighbors.Add(GetElementOrDefault(next, rowIdx + 1));
            neighbors.Add(GetElementOrDefault(above, rowIdx + aboveOrBelowOffset + 0));
            neighbors.Add(GetElementOrDefault(above, rowIdx + aboveOrBelowOffset + 1));
            neighbors.Add(GetElementOrDefault(below, rowIdx + aboveOrBelowOffset + 0));
            neighbors.Add(GetElementOrDefault(below, rowIdx + aboveOrBelowOffset + 1));

            neighbors.RemoveAll(t => t == null);

            return neighbors;
        }

        public Tile GetNextTileInPath(Tile tile)
        {
            for (int i = 0; i < _path.Count; i++)
            {
                if (tile == _path[i])
                {
                    return GetElementOrDefault(_path, i + 1);
                }
            }
            return null;
        }

        private T GetElementOrDefault<T>(List<T> list, int idx)
        {
            if (idx > 0 && idx < list.Count)
            {
                return list[idx];
            }

            return default;
        }
    }
}