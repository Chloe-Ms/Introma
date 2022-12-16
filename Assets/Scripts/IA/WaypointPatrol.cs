using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints; //Les points de suivi
    private int currentWaypoint;


    void Start()
    {
        if (waypoints.Length > 0){
            navMeshAgent.SetDestination(waypoints[0].position);
        }
    }

    void Update()
    {
        //Si l'ennemi a atteint le point, on change de point
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && waypoints.Length > 0){
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMeshAgent.SetDestination (waypoints[currentWaypoint].position);
        }
    }
}
