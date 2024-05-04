using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : PathFinder
{
    public override void Search(TileLogic start, TileLogic objective)
    {
        int iterationCount = 0;
        tilesSearch = new List<TileLogic>();

        //tilesSearch.Add(start);
        Board.Instance.ClearSearch();
        TileLogic current;

        List<TileLogic> openSet = new List<TileLogic>(); 
        openSet.Add(start);
        start.CostFromOrigin = 0;

        while (openSet.Count > 0)
        {
            openSet.Sort((x, y) => x.Score.CompareTo(y.Score));
            current = openSet[0];
            tilesSearch.Add(current);

            if (current == objective){
                //Debug.Log("Objetive found");
                break;
            }
            openSet.RemoveAt(0);

            for (int i = 0; i < Board.Directions.Length; i++)
            {
                TileLogic next = Board.GetTile(current.Position + Board.Directions[i]); 
                iterationCount++;

                if (next == null || next.CostFromOrigin <= current.CostFromOrigin + next.MoveCost)
                {
                    continue;
                }
                next.CostFromOrigin = current.CostFromOrigin + next.MoveCost;
                next.Previous = current;
                next.CostToObjective = Vector2Int.Distance(next.Position, objective.Position) * 2;
                next.Score = next.CostToObjective + next.CostFromOrigin;

                if (!tilesSearch.Contains(next))
                {
                    openSet.Add(next);

                }
                // Board.Instance.PaintTile(next.Position, Color.yellow);

            }
        }

    }
}
