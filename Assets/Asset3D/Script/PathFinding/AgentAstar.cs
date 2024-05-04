using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AgentAstar : MonoBehaviour
{
    #region Fields

    public const float DistanceToChangeWaypoint = 2f;

    public AStar Astar;

    public float Speed = 2f;

    public Vector2Int CurrentPosition;

    public Transform Objective;

    bool followingPath;

    List<TileLogic> path;

    Rigidbody rb;

    int currentWaypoint;
    #endregion
    #region Unity Events

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!followingPath)
        {
            return; 
        }
        Move();
        CheckWaypoint();
    }
    #endregion

    #region Methods
    public void FollowPath()
    {
        CancelInvoke();
        InvokeRepeating("BuildPath", 0, 2); ;
    }

    void BuildPath()
    {
        TileLogic currentTile = Board.Instance.WorldPositionToTile(rb.position);
        TileLogic targetTile = Board.Instance.WorldPositionToTile(Objective.position);
        currentWaypoint = 0;

        Astar.Search(currentTile, targetTile);
        path = Astar.BuildPath(targetTile);
        followingPath = true;
    }

    void Move()
    {
        Vector3 targetDirection = path[currentWaypoint].WorldPosition - rb.position;
        rb.MovePosition(rb.position + (targetDirection * Time.fixedDeltaTime* Speed));
    }

    void CheckWaypoint()
    {
        if (Vector3.Distance(rb.position, path[currentWaypoint].WorldPosition) < DistanceToChangeWaypoint)
        {
            CurrentPosition = path[currentWaypoint].Position;
            currentWaypoint++;
            if (currentWaypoint == path.Count)
            {
                followingPath = false;
            }
        }

    }
    #endregion
}
