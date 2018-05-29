using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Mapping;
using Hexen;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material Material { get; set; }
    public TileType TileType { get; set; }

    public bool IsEmpty = true;

}
