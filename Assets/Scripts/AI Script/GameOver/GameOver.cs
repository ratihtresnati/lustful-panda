using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public CatchSensor catchSensor;
    public bool GameEnd = false;

    public AIPatrolling ZookeeperSet;
    public Transform Panda;
    public Transform Zokeeper;
    public float speed = 5f;

    private void Update()
    {
        if (catchSensor.catchPlayer)
        {
            GameEnd = false;

            ZookeeperSet.RunSpeed = 0f;

            Vector3 targetDir = Zokeeper.position - Panda.position;

            float singleStep = speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(Panda.forward, targetDir, singleStep, 0);

            Panda.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
