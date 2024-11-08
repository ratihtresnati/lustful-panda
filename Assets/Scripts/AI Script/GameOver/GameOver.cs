using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public CatchSensor catchSensor;
    public bool GameEnd = false;

    private void Update()
    {
        if (catchSensor.catchPlayer)
        {
            GameEnd = true;

            //SceneManager.LoadScene(2);
        }
    }
}
