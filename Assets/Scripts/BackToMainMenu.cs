using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    void Update()
    {
    }
    public void ButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
