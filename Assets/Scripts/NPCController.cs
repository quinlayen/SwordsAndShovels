using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggroRange = 10f;
    public Transform[] waypoints;

    private int index;
    private float speed, agentSpeed;
    private Transform player;

    private Animator anim;
    private NavMeshAgent agent;

    private void Awake()
    {
        {
            //anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            if (agent != null) { agentSpeed = agent.speed; }

            player = GameObject.FindGameObjectWithTag("Player").transform;
            index = Random.Range(0, waypoints.Length);

            InvokeRepeating("Tick", 0, 0.5f);

            if (waypoints.Length > 0) { InvokeRepeating("patrol", 0, patrolTime); }
        }
    }

    private void patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1; // if true then 0 else index + 1
    }

    private void Tick()
    {
        agent.speed = agentSpeed / 2;
        agent.destination = waypoints[index].position;
        if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            Debug.Log("Close");
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }

    }
}
