using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

namespace Hexen
{
    public class MapManager : MonoBehaviour
    {
        public float tileSpacing;
        private List<List<Tile>> tiles = new List<List<Tile>>();
        private List<Tile> path = new List<Tile>();
        public Tile StartTile;
        public Tile EndTile;

        protected MapManager()
        {

        }

        // Use this for initialization
        void Start()
        {
            TextAsset ta = Resources.Load<TextAsset>("MapData/Map01");
            parseMapFile(ta.text);

            DynamicGI.UpdateEnvironment();
        }

        private void parseMapFile(string map)
        {
            GameObject parent = this.gameObject;
            float spacing = tileSpacing;

            Mesh mesh = new Mesh();
            float outerRadius = 0.5f;
            float innerRadius = outerRadius * Mathf.Sqrt(3) / 2;
            {
                List<Vector3> vertices = new List<Vector3>();
                List<Vector2> uvs = new List<Vector2>();

                vertices.Add(new Vector3(0, 0, 0));
                vertices.Add(new Vector3(0, 0, outerRadius));
                vertices.Add(new Vector3(innerRadius, 0, outerRadius / 2f));
                vertices.Add(new Vector3(innerRadius, 0, -outerRadius / 2f));
                vertices.Add(new Vector3(0, 0, -outerRadius));
                vertices.Add(new Vector3(-innerRadius, 0, -outerRadius / 2f));
                vertices.Add(new Vector3(-innerRadius, 0, outerRadius / 2f));

                //uv needs x/z not x/y
                //uvs.AddRange(vertices.Select(v => (Vector2)v).ToList());
                foreach (var v in vertices)
                {
                    uvs.Add(new Vector2(v.x, v.z));
                }

                mesh.SetVertices(vertices);
                mesh.SetUVs(0, uvs);
                mesh.SetIndices(new int[] {
                0,1,2,
                0,2,3,
                0,3,4,
                0,4,5,
                0,5,6,
                0,6,1
            }, MeshTopology.Triangles, 0);

                mesh.RecalculateNormals();
                mesh.UploadMeshData(true);
            }
            StartTile = null;
            EndTile = null;

            GameObject prefab = Resources.Load<GameObject>("Prefabs/Tile");
            List<string> lines = map.Split('\n').ToList();

            for (int lineIdx = 0; lineIdx < lines.Count; lineIdx++)
            {
                string line = lines[lineIdx].Trim();
                if (line == string.Empty) continue;
                List<string> tileData = line.Split(' ').ToList();
                List<Tile> tileRow = new List<Tile>();
                this.tiles.Add(tileRow);

                for (int rowIdx = 0; rowIdx < tileData.Count; rowIdx++)
                {
                    char tileDatum = char.Parse(tileData[rowIdx]);

                    Tile tile = TileProvider.getTile(tileDatum, prefab);
                    tile.GetComponent<MeshFilter>().mesh = mesh;
                    tile.GetComponent<MeshCollider>().sharedMesh = mesh;

                    tile.transform.SetParent(parent.transform);

                    Vector3 newPosition = new Vector3();
                    newPosition.x = (innerRadius * 2 + spacing) * rowIdx;
                    newPosition.z = -(outerRadius * 2 + spacing) * (3f / 4f) * lineIdx;

                    if (lineIdx % 2 == 0)
                    {
                        newPosition.x -= (innerRadius * 2 + spacing) / 2;
                    }
                    tile.transform.SetPositionAndRotation(newPosition, tile.transform.rotation);

                    tile.GetComponent<Renderer>().material = tile.Material;
                    tileRow.Add(tile);

                    if (tile.TileType == Assets.Scripts.Mapping.TileType.Start)
                    {
                        StartTile = tile;
                    }
                    if (tile.TileType == Assets.Scripts.Mapping.TileType.End)
                    {
                        EndTile = tile;
                    }
                }
            }

            // create navmesh
            if (StartTile == null || EndTile == null)
            {
                throw new System.Exception("Map has no defined start and/or end tile.");
            }

            path.Add(StartTile);
            while (true)
            {
                var neighbors = GetTileNeighbors(path.Last());
                var filteredNeighbors = neighbors.Where(t =>
                    t.TileType == Assets.Scripts.Mapping.TileType.Path
                    && !path.Contains(t)).ToList();
                var nextTile = filteredNeighbors.FirstOrDefault();

                if (nextTile != null)
                {
                    path.Add(nextTile);
                }
                else if (neighbors.Contains(EndTile))
                {
                    path.Add(EndTile);
                    break;
                }
                else
                {
                    throw new System.Exception("No path found from Start to End Tile.");
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