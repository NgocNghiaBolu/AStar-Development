using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    List<AgentAstar> agents;

    private void Awake()
    {
        agents = new List<AgentAstar>();
        agents.AddRange(GetComponentsInChildren<AgentAstar>());
        Invoke("VeryLateStart", 3);
    }

    void VeryLateStart()
    {
        foreach(AgentAstar agent in agents)
        {
            agent.FollowPath();
        }
    }
}
