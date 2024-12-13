using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrollingGroup : MonoBehaviour
{
    public GroupTrigger GroupTrigger;

    [SerializeField] private AISensor Sensor;

    [SerializeField] private PlayerController PlayerController;

    private AIAnimatorController AIAnimatorController;

    [SerializeField] private Transform player;
    private GameObject _playerPanda;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float pandaRotSpeed = 5f;

    private CatchSensor catchSensor;

    public float RunSpeed = 4f;

    NavMeshAgent agent;
    public Transform[] patrolPoint;
    int patrolPointIndex;
    Vector3 target;
    ZooKeeperState currentState;

    [SerializeField] private float idleTime = 2f;
    [SerializeField] private float idleTimeAfterLosePlayer = 3f;
    float idleTimer = 0f;

    [SerializeField] private Vector3 pl;

    // Start is called before the first frame update
    void Start()
    {
        _playerPanda = GameObject.Find("Panda Bayik");
        //player = _playerPanda.transform;
        //GameOver = FindObjectOfType<GameOver>();
        AIAnimatorController = GetComponent<AIAnimatorController>();
        agent = GetComponent<NavMeshAgent>();
        Sensor = GetComponent<AISensor>();

        catchSensor = GetComponent<CatchSensor>();
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
                AudioManager.Instance.Chase();
                break;
            case ZooKeeperState.Catch:
                Catch();
                AIAnimatorController.Catch();
                break;
        }

    }

    private void Catch()
    {
        player.position = transform.TransformPoint(pl);


        GetComponent<NavMeshAgent>().speed = 0;

        Vector3 targetDir = transform.position - player.position;

        float singleStep = pandaRotSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(player.forward, -targetDir, singleStep, 0);

        newDir.y = 0;

        player.rotation = Quaternion.LookRotation(newDir);
        gameObject.transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    private void AfterChase()
    {
        GetComponent<NavMeshAgent>().speed -= 1;

        if (GetComponent<NavMeshAgent>().speed <= 0)
        {
            idleTimer = idleTimeAfterLosePlayer;
            currentState = ZooKeeperState.Search;
        }
        if (Sensor.canSeePlayer || GroupTrigger.GroupCanSee)
        {
            //GroupTrigger.groupCanSee = true;
            currentState = ZooKeeperState.Chase;

        }
    }

    void Chase()
    {
        if (PlayerController.InBox)
        {
            StartCoroutine(BecomeBox());
        }

        GetComponent<NavMeshAgent>().speed = RunSpeed;
        agent.SetDestination(player.position);

        //Debug.Log(PlayerController.GameOver);

        if (catchSensor.catchPlayer && !PlayerController.IsJump)
        {

            GetComponent<NavMeshAgent>().speed = 0;
            PlayerController.GameOver = true;
            currentState = ZooKeeperState.Catch;
        }

        else if (PlayerController.GameOver) 
        {
            GetComponent<NavMeshAgent>().speed = 0;
            currentState = ZooKeeperState.Idle;
        }

        if (!Sensor.canSeePlayer)
        {
            //idleTimer = 2f;
            PlayerController.PlayerSee = false;
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

        if (Sensor.canSeePlayer || GroupTrigger.GroupCanSee)
        {
            //GroupTrigger.groupCanSee = true;
            currentState = ZooKeeperState.Chase;

        }
        
    }

    private void Search()
    {

        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            patrolPointIndex = 0;
            target = patrolPoint[patrolPointIndex].position;
            agent.SetDestination(target);

            AudioManager.Instance.PlayBGM();

            currentState = ZooKeeperState.Patrol;
        }

        if (Sensor.canSeePlayer || GroupTrigger.GroupCanSee)
        {
            //GroupTrigger.groupCanSee = true;
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
        if (Sensor.canSeePlayer || GroupTrigger.GroupCanSee)
        {
            //GroupTrigger.groupCanSee = true;
            currentState = ZooKeeperState.Chase;
        }
    }

    IEnumerator BecomeBox()
    {
        PlayerController.SmokeVFX.Play();
        yield return new WaitForSeconds(0.3f);
        PlayerController.PlayerSee = true;
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
