using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    public float Speed = 5;
    public float avoidSpeed = 150f
        ;
    public Transform Target;

    public PathMaker PathMaker;

    const float distanceToChangeWaypoint = 5f;

    Rigidbody rb;
    Sensor senSor;

    List<Transform> path { get { return PathMaker.Waypoints; } }
    int currentWaypoint = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        senSor = GetComponent<Sensor>();
    }

    void FixedUpdate() 
    {
        float avoid = senSor.Check();
        if (avoid == 0)
        {
            StandardSteer();
        }
        else
        {
            AvoidSteer(avoid);
        }
        Move();
        CheckWaypoint();

    }

    void Move()
    {
        rb.MovePosition(rb.position + (transform.forward * Speed * Time.deltaTime));
    }

    void StandardSteer()
    {
        Vector3 targetDirection = path[currentWaypoint].position - rb.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.fixedDeltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void AvoidSteer(float avoid)
    {
        transform.RotateAround(transform.position, transform.up, avoidSpeed * Time.fixedDeltaTime * avoid);
    }

    void CheckWaypoint()
    {
        if(Vector3.Distance(rb.position, path[currentWaypoint].position) < distanceToChangeWaypoint)
        {
            currentWaypoint++;
            Debug.Log(currentWaypoint);
            if (currentWaypoint == path.Count)
            {
                currentWaypoint = 0;
            }
        }
    }

}
