using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolling : MonoBehaviour
{
    [SerializeField] private AISensor Sensor;

    [SerializeField] private PlayerController PlayerController;

    public AIAnimatorController AIAnimatorController;

    [SerializeField] private Transform player;
    private GameObject _playerPanda;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float pandaRotSpeed = 5f;
    
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
        _playerPanda = GameObject.Find("Panda Bayik");
        player = _playerPanda.transform;
        //GameOver = FindObjectOfType<GameOver>();
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
                AIAnimatorController.Search();
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
            case ZooKeeperState.Catch:
                Catch();
                AIAnimatorController.Catch();
                break;
        }

    }

    private void Catch()
    {
        GetComponent<NavMeshAgent>().speed = 0;

        Vector3 targetDir = transform.position - player.position;

        float singleStep = pandaRotSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(player.forward, -targetDir, singleStep, 0);

        newDir.y = 0;

        player.rotation = Quaternion.LookRotation(newDir);
    }

    private void AfterChase()
    {
        GetComponent<NavMeshAgent>().speed -= 1;

        if (GetComponent<NavMeshAgent>().speed <= 0)
        {
            idleTimer = idleTimeAfterLosePlayer;
            currentState = ZooKeeperState.Search;
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

        Debug.Log(PlayerController.GameOver);

        if (PlayerController.GameOver)
        {
            GetComponent<NavMeshAgent>().speed = 0;
            currentState = ZooKeeperState.Catch;
        }

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
            if (patrolPoint != null && patrolPoint.Length > 0)
            {
                target = patrolPoint[patrolPointIndex].position;
                agent.SetDestination(target);

                GetComponent<NavMeshAgent>().speed = walkSpeed;
                currentState = ZooKeeperState.Patrol;
            }
        }

        if (Sensor.canSeePlayer)
        {
            currentState = ZooKeeperState.Chase;

        }
    }

    private void Search()
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
        AfterChase,
        Catch
    }

}
