using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public static Vector2Int[] Directions = new Vector2Int[4]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left,
    };

    //public Tilemap Tilemap;
    //public Vector3Int Size;
    public Vector2Int MinSize;
    public Vector2Int MaxSize;
    

    public Dictionary<Vector2Int, TileLogic> Tiles;

    public float NodeSize;

    public bool DrawGizmos;

    public Transform ObstacleHolder;

    void Awake()
    {
        Instance = this;
        Tiles = new Dictionary<Vector2Int, TileLogic>(); 
        CreateTileLogics(); 
        Debug.Log(Tiles.Count);
    }

    private void OnDrawGizmos()
    {
        if(!DrawGizmos || Tiles == null || Tiles.Count == 0)
        {
            return;
        }
        Gizmos.color = Color.red;
        foreach(TileLogic t in Tiles.Values)
        {
            Gizmos.DrawCube(t.WorldPosition, Vector3.one * (NodeSize / 2));
        }
    }

    public static TileLogic GetTile(Vector2Int position)
    {
        TileLogic tile;
        if (Instance.Tiles.TryGetValue(position, out tile))
            return tile;
        return null;
    }

    public void ClearSearch()
    {
        foreach(TileLogic t in Tiles.Values)
        {
            t.CostFromOrigin = int.MaxValue;
            t.CostToObjective = int.MaxValue;
            t.Score = int.MaxValue;
            t.Previous = null;
        }
    }

    public TileLogic WorldPositionToTile(Vector3 position)
    {
        Vector3 nodePosition = position / NodeSize;
        Vector2Int pos2D = new Vector2Int(Mathf.RoundToInt(nodePosition.x), Mathf.RoundToInt(nodePosition.z));
        TileLogic toReturn = GetTile(pos2D);
        Debug.LogFormat("From:{0}, to {1}, tile:{2}", position, pos2D,toReturn);
        return toReturn;
    }

    void CreateTileLogics()
    {
        for (int x = MinSize.x; x < MaxSize.x; x++)
        {
            for (int y = MinSize.y; y < MaxSize.y; y++)
            {
                TileLogic tile = new TileLogic();
                tile.Position = new Vector2Int(x, y);
                Tiles.Add(tile.Position, tile);
                SetTile(tile);
            }
        }

    }

    void SetTile(TileLogic tileLogic)
    {
        tileLogic.MoveCost = 1;
        tileLogic.WorldPosition = new Vector3(tileLogic.Position.x, 0, tileLogic.Position.y) * NodeSize;
        CheckCollision(tileLogic);
    }

    void CheckCollision(TileLogic tileLogic) 
    { 
        Collider[] colliders = Physics.OverlapSphere(tileLogic.WorldPosition, NodeSize / 2);
        foreach(Collider colli in colliders)
        {
            if(colli.transform.parent == ObstacleHolder)
            {
                //Debug.Log("Obstacle at" + tileLogic.WorldPosition);
                tileLogic.MoveCost = int.MaxValue;  
            }
        }
    }
}
