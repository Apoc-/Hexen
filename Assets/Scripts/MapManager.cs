using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Hexen
{
    public class MapManager : Singleton<MapManager>
    {
        protected MapManager()
        {
        }

        // Use this for initialization
        void Start()
        {
            TextAsset ta = Resources.Load<TextAsset>("MapData/Map01.txt");
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void parseMapFile(string map)
        {
            GameObject parent = this.gameObject;

            Mesh mesh = new Mesh();
            {
                List<Vector3> vertices = new List<Vector3>();
                List<Vector2> uvs = new List<Vector2>();

                float aussenradius = 0.5f;
                float innenradius = aussenradius * Mathf.Pow(3, 1f / 3f) / 2;

                vertices.Add(new Vector3(0, 0, 0));
                vertices.Add(new Vector3(0, aussenradius, 0));
                vertices.Add(new Vector3(innenradius, aussenradius / 2f, 0));
                vertices.Add(new Vector3(innenradius, -aussenradius / 2f, 0));
                vertices.Add(new Vector3(0, -aussenradius, 0));
                vertices.Add(new Vector3(-innenradius, -aussenradius / 2f, 0));
                vertices.Add(new Vector3(-innenradius, aussenradius / 2f, 0));

                uvs.AddRange(vertices.Select(v => (Vector2)v).ToList());

                mesh.SetVertices(vertices);
                mesh.SetUVs(0, uvs);
                mesh.SetIndices(new int[] {
                0,1,2,
                0,2,3,
                0,3,4,
                0,4,5,
                0,5,6
            }, MeshTopology.Triangles, 0);

                mesh.UploadMeshData(true);
            }


            map.Split('\n');

            GameObject tile = Instantiate(Resources.Load("Prefabs/Tile", typeof(GameObject))) as GameObject;
            tile.GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}