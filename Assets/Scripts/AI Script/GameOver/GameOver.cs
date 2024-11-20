using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PlayerController PlayerControl;

    public bool GameEnd = false;
    public bool PlayerCatch = false;


    public bool PlayerSee = false;

    private void Update()
    {
        //layerControl = FindObjectOfType<PlayerController>();

        Debug.Log(PlayerCatch);

        if (PlayerCatch)
        {
            StartCoroutine(GameOverMoment());
            
        }
    }

    IEnumerator GameOverMoment()
    {
        if (PlayerControl.IsJump)
        {
            GameEnd = false;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            GameEnd = true;
        }

    }
}
