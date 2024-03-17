using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite
{
    public Vector3Int TilePosition { get; set; }
    public Vector3 WorldPosition { get; set; }
    public ConstructionSite Level { get; set; }
    public TowerType TowerType { get; set; }
}
