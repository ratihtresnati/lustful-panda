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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        CloseMenu();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CloseMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been closed.");
    }

    //menu
    [SerializeField] private GameObject menuInfo;
    [SerializeField] private GameObject menuPause;

    public void ShowMenuInfo()
    {
        menuInfo.SetActive(true);
        menuPause.SetActive(false);
        
        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
    }
    public void ShowMenuPause()
    {
        menuInfo.SetActive(false);
        menuPause.SetActive(true);
        
        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void CloseMenu()
    {
        menuInfo.SetActive(false);
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
}

