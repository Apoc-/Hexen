using System.Collections.Generic;
using System.Linq;
using Systems.GameSystem;
using UnityEngine;

namespace Systems.MapSystem
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private TextAsset currentMap;

        private float TileSpacing = 0f;
        private float outerRadius = 0.5f;
        private float innerRadius = 0.5f * Mathf.Sqrt(3) / 2;
        public float BaseHeight { get; } = -2f;

        private float RandomHeightOffset = 0.0f;

        private List<List<Tile>> tiles = new List<List<Tile>>();
        private List<Tile> path = new List<Tile>();
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
            parseMapFile(currentMap.text);

            DynamicGI.UpdateEnvironment();
        }

        private void parseMapFile(string map)
        {
            GameObject parent = gameObject;
            float spacing = TileSpacing;

            Mesh mesh = new Mesh();
            
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
                tiles.Add(tileRow);

                for (int rowIdx = 0; rowIdx < tileData.Count; rowIdx++)
                {
                    char tileDatum = char.Parse(tileData[rowIdx]);

                    Tile tile = TileProvider.GetTile(tileDatum, prefab);

                    tile.transform.SetParent(parent.transform);

                    Vector3 newPosition = new Vector3();
                    newPosition.x = (innerRadius * 2 + spacing) * rowIdx;
                    newPosition.z = -(outerRadius * 2 + spacing) * (3f / 4f) * lineIdx;

                    var seed = (int)System.DateTime.Now.Ticks;
                    var rnd = new System.Random(seed);
                    float r = rnd.Next((int)(RandomHeightOffset*100)) / 100.0f;

                    newPosition.y = BaseHeight + r + tile.DeltaHeight;
                    

                    if (lineIdx % 2 == 0)
                    {
                        newPosition.x -= (innerRadius * 2 + spacing) / 2;
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
            MapWidth = tiles[0].Count * innerRadius * 2;
            MapHeight = tiles.Count * outerRadius * 2;
            LeftBound = tiles[0][0].transform.position.x - innerRadius;
            UpperBound = tiles[0][0].transform.position.z - outerRadius;
        }

        private void CreateNavMesh()
        {
            if (StartTile == null || EndTile == null)
            {
                throw new System.Exception("Map has no defined start and/or end tile.");
            }

            StartTile.NumberInPath = 0;
            path.Add(StartTile);
            while (true)
            {
                var neighbors = GetTileNeighbors(path.Last());
                var filteredNeighbors = neighbors.Where(t =>
                    t.TileType == TileType.Path
                    && !path.Contains(t)).ToList();
                var nextTile = filteredNeighbors.FirstOrDefault();

                if (nextTile != null)
                {
                    nextTile.NumberInPath = path.Last().NumberInPath + 1;
                    path.Add(nextTile);
                }
                else if (neighbors.Contains(EndTile))
                {
                    EndTile.NumberInPath = path.Last().NumberInPath + 1;
                    path.Add(EndTile);
                    break;
                }
                else
                {
                    throw new System.Exception("No path found from Start to End Tile.");
                }
            }

            if (GameSettings.Debug)
            {
                for (int i = 0; i < path.Count-1; i++)
                {
                    var t1 = path[i];
                    var t2 = path[i + 1];

                    Debug.DrawLine(t1.GetTopCenter(), t2.GetTopCenter(), Color.white, 9999);
                }
            }
        }

        private List<Tile> GetTileNeighbors(Tile tile)
        {
            List<Tile> neighbors = new List<Tile>();
            int lineIdx = 0;
            int rowIdx = 0;
            for (lineIdx = 0; lineIdx < tiles.Count; lineIdx++)
            {
                for (rowIdx = 0; rowIdx < tiles[lineIdx].Count; rowIdx++)
                {
                    if (tiles[lineIdx][rowIdx] == tile)
                    {
                        goto doublebreak;
                    }
                }
            }

        doublebreak:

            List<Tile> above = new List<Tile>();
            List<Tile> next = new List<Tile>(tiles[lineIdx]);
            List<Tile> below = new List<Tile>();
            if (lineIdx > 0)
            {
                above.AddRange(tiles[lineIdx - 1]);
            }
            if (lineIdx < tiles.Count - 1)
            {
                below.AddRange(tiles[lineIdx + 1]);
            }

            // if (lineIdx % 2 == 0)
            // offset to the left
            int aboveOrBelowOffset = lineIdx % 2 == 0
                ? -1
                : 0;

            neighbors.Add(getElementOrDefault(next, rowIdx - 1));
            neighbors.Add(getElementOrDefault(next, rowIdx + 1));
            neighbors.Add(getElementOrDefault(above, rowIdx + aboveOrBelowOffset + 0));
            neighbors.Add(getElementOrDefault(above, rowIdx + aboveOrBelowOffset + 1));
            neighbors.Add(getElementOrDefault(below, rowIdx + aboveOrBelowOffset + 0));
            neighbors.Add(getElementOrDefault(below, rowIdx + aboveOrBelowOffset + 1));

            neighbors.RemoveAll(t => t == null);

            return neighbors;
        }

        public Tile GetNextTileInPath(Tile tile)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (tile == path[i])
                {
                    return getElementOrDefault(path, i + 1);
                }
            }
            return null;
        }

        private T getElementOrDefault<T>(List<T> list, int idx)
        {
            if (idx > 0 && idx < list.Count)
            {
                return list[idx];
            }

            return default(T);
        }
    }
}