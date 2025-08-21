using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void KeyframeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void BlendTreeScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ProceduralScene()
    { 
        SceneManager.LoadScene(3);
    }
}
