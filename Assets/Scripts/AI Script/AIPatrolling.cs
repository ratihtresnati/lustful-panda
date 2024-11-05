using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolling : MonoBehaviour
{
    public AISensor Sensor;

    [SerializeField] private Transform player;
    [SerializeField] private float runSpeed = 4f;

    NavMeshAgent agent;
    public Transform[] patrolPoint;
    int patrolPointIndex;
    Vector3 target;
    ZooKeeperState currentState;

    [SerializeField] private float idleTime = 2f;
    [SerializeField] private float idleTimeAfterLosePlayer = 5f;
    float idleTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Sensor = GetComponent<AISensor>();

        currentState = ZooKeeperState.Idle;
    }

    // Update is called once per frame
    
    void Update()
    {

        switch (currentState)
        {
            case ZooKeeperState.Idle:
                Idle();
                break;
            case ZooKeeperState.Patrol:
                Patrol();
                break;
            case ZooKeeperState.Chase:
                Chase();
                break;
        }

    }

    void Chase()
    {
        GetComponent<NavMeshAgent>().speed = runSpeed;
        agent.SetDestination(player.position);
        if (!Sensor.canSeePlayer)
        {
            idleTimer = idleTimeAfterLosePlayer;
            currentState = ZooKeeperState.Idle;

        }
    }

    private void Idle()
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            target = patrolPoint[patrolPointIndex].position;
            agent.SetDestination(target);

            currentState = ZooKeeperState.Patrol;
        }

        if (Sensor.canSeePlayer)
        {
            currentState = ZooKeeperState.Chase;
        }
    }
    private void Patrol()
    {
        if (agent.remainingDistance <= 0.5f)
        {
            patrolPointIndex++;
            idleTimer = idleTime;
            
            if (patrolPointIndex == patrolPoint.Length)
            {
                patrolPointIndex = 0;
                currentState = ZooKeeperState.Idle;
            }
                currentState = ZooKeeperState.Idle;

            if (Sensor.canSeePlayer)
            {
                currentState = ZooKeeperState.Chase;
            }

        }
    }

    public enum ZooKeeperState
    {
        Idle,
        Patrol,
        Chase
    }

}
