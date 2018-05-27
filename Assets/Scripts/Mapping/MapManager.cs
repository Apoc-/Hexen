using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Hexen
{
    public class MapManager : Singleton<MapManager>
    {
        public float tileSpacing;

        protected MapManager()
        {
        }

        // Use this for initialization
        void Start()
        {
            TextAsset ta = Resources.Load<TextAsset>("MapData/Map01");
            parseMapFile(ta.text);
        }

        // Update is called once per frame
        void Update()
        {

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

                uvs.AddRange(vertices.Select(v => (Vector2)v).ToList());

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

                mesh.UploadMeshData(true);
            }

            GameObject prefab = Resources.Load<GameObject>("Prefabs/Tile");
            List<string> lines = map.Split('\n').ToList();

            for (int lineIdx = 0; lineIdx < lines.Count; lineIdx++)
            {
                string line = lines[lineIdx].Trim();
                if (line == string.Empty) continue;
                List<string> tileData = line.Split(' ').ToList();

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
                }
            }

        }
    }
}