using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaker : MonoBehaviour
{
    public List<Transform> Waypoints;
    [ContextMenu("Create Path")]

    void CreatePath()
    {
        Waypoints = new List<Transform>();
        Waypoints.AddRange(GetComponentsInChildren<Transform>());
        Waypoints.Remove(this.transform);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (Waypoints != null && Waypoints.Count > 0)
        {
            for (int i = 1; i < Waypoints.Count; i++)
            {
                Gizmos.DrawLine(Waypoints[i - 1].position, Waypoints[i].position);

            }
            Gizmos.DrawLine(Waypoints[0].position, Waypoints[Waypoints.Count - 1].position);
        }
    }
}
