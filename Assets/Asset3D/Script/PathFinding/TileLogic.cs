using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic
{
    public Vector2Int Position;
    public Vector3 WorldPosition;
    public Color Color;
    public float distance;
    public TileLogic Previous;
    public int MoveCost;
    public float CostFromOrigin;
    public float CostToObjective;
    public float Score;
    public bool Occupied;
}
