using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PlayerController PlayerControl;

    public CatchSensor catchSensor;
    public bool GameEnd = false;
    public bool GameEndT = false;


    public bool PlayerSee = false;

    private void Update()
    {
        PlayerControl = FindObjectOfType<PlayerController>();

        if (catchSensor.catchPlayer)
        {
            StartCoroutine(GameOverMoment());
            
        }
    }

    IEnumerator GameOverMoment()
    {
        if (PlayerControl.IsJump)
        {
            GameEnd = false;
            //GameEndT = true;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            GameEnd = true;
            //GameEndT = true;
        }

    }
}
