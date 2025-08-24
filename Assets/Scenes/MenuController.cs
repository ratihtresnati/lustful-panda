using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Menu menu;

    private void Awake()
    {
        menu = gameObject.GetComponent<Menu>();
    }
    private void Update()
    {
        if (InputManager.instance.PauseInput)
        {
            menu.ShowMenuPause();
            menu.PauseGame();
        }

        if (InputManager.instance.ResumeInput || InputManager.instance.CloseMenu)
        {
            menu.CloseMenu();
        }

        if (InputManager.instance.MenuInfo)
        {
            menu.ShowMenuInfo();
            menu.PauseGame();
        }

        // dipake untuk restart
        if (InputManager.instance.MenuQuest)
        {
            menu.RestartGame();
        }
    }
}
