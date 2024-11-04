using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolling : MonoBehaviour
{
    public AISensor Sensor;

    NavMeshAgent agent;
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Sensor = GetComponent<AISensor>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position;

        if (Sensor.canSeePlayer == true)
        {
            agent.SetDestination(target);
        }
    }
}
