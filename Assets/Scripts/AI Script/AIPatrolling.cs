using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolling : MonoBehaviour
{
    public AISensor Sensor;
    [SerializeField] private Transform player;

    NavMeshAgent agent;
    public Transform[] patrolPoint;
    int patrolPointIndex;
    Vector3 target;

    [SerializeField] private float idleTime = 2f;
    [SerializeField] private float idleTimeAfterLosePlayer = 5f;
    float idleTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Sensor = GetComponent<AISensor>();

        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {

        if (Sensor.canSeePlayer)
        {
            Debug.Log("going to player");
            agent.SetDestination(player.position);
            idleTimer = idleTimeAfterLosePlayer;
            
        }
        else
        
        {

            if (Vector3.Distance(transform.position, patrolPoint[patrolPointIndex].position) < 2f)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }

            UpdateDestination();

        }
        
    }


    private void UpdateDestination()
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            target = patrolPoint[patrolPointIndex].position;
            agent.SetDestination(target);
        }
    }
    private void IterateWaypointIndex()
    {
        patrolPointIndex++;
        idleTimer = idleTime;
        if (patrolPointIndex == patrolPoint.Length)
        {
            patrolPointIndex = 0;
        }
    }

}
