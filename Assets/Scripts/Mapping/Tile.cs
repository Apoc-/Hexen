using System.Collections;
using System.Collections.Generic;
using Hexen;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material Material { get; set; }
    public TileType TileType { get; set; }

    public bool IsEmpty = true;

    public float Height { get; set; }

    public Vector3 GetTopCenter()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var h = meshFilter.mesh.bounds.size.y;
        return transform.position + new Vector3(0, h, 0);
    }

}
