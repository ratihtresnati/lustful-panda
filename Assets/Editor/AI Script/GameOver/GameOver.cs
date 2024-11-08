using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public CatchSensor catchSensor;

    private void Update()
    {
        if (catchSensor.catchPlayer)
        {
            
            SceneManager.LoadScene(2);
        }
    }
}
