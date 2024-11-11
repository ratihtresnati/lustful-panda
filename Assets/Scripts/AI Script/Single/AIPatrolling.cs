using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolling : MonoBehaviour
{
    public AISensor Sensor;

    public AIAnimatorController AIAnimatorController;

    [SerializeField] private Transform player;
    [SerializeField] private float walkSpeed = 3f;
    
    public float RunSpeed = 4f;

    NavMeshAgent agent;
    public Transform[] patrolPoint;
    int patrolPointIndex;
    Vector3 target;
    ZooKeeperState currentState;

    [SerializeField] private float idleTime = 2f;
    [SerializeField] private float idleTimeAfterLosePlayer = 3f;
    float idleTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        AIAnimatorController = GetComponent<AIAnimatorController>();
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
                AIAnimatorController.Idle();
                break;
            case ZooKeeperState.Search:
                Search();
                AIAnimatorController.Idle();
                break;
            case ZooKeeperState.Patrol:
                Patrol();
                AIAnimatorController.Walk();
                break;
            case ZooKeeperState.AfterChase:
                AfterChase();
                break;
            case ZooKeeperState.Chase:
                Chase();
                AIAnimatorController.Run();
                break;
        }

    }

    private void AfterChase()
    {
        GetComponent<NavMeshAgent>().speed -= 1;

        if (GetComponent<NavMeshAgent>().speed <= 0)
        {
            idleTimer = idleTimeAfterLosePlayer;
            currentState = ZooKeeperState.Idle;
        }
        if (Sensor.canSeePlayer)
        {
            currentState = ZooKeeperState.Chase;

        }
    }

    void Chase()
    {
        GetComponent<NavMeshAgent>().speed = RunSpeed;
        agent.SetDestination(player.position);
        if (!Sensor.canSeePlayer)
        {
            //idleTimer = 2f;

            currentState = ZooKeeperState.AfterChase;

        }
    }

    private void Idle()
    {

        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            target = patrolPoint[patrolPointIndex].position;
            agent.SetDestination(target);

            GetComponent<NavMeshAgent>().speed = walkSpeed;
            currentState = ZooKeeperState.Patrol;
        }

        if (Sensor.canSeePlayer)
        {
            currentState = ZooKeeperState.Chase;

        }
    }

    private void Search()
    {

        GetComponent<NavMeshAgent>().speed = walkSpeed;
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
        GetComponent<NavMeshAgent>().speed = walkSpeed;
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


        }
            if (Sensor.canSeePlayer)
            {
                currentState = ZooKeeperState.Chase;
            }
    }

    public enum ZooKeeperState
    {
        Idle,
        Patrol,
        Chase,
        Search,
        AfterChase
    }

}
