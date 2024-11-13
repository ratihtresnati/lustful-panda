using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public CatchSensor catchSensor;
    public bool GameEnd = false;
    public bool GameEndT = false;


    public bool PlayerSee = false;

    private void Update()
    {
        if (catchSensor.catchPlayer)
        {
            GameEnd = false;
            GameEndT = true;
        }
    }
}
