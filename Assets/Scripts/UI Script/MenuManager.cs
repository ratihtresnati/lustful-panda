using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingMenu;
    [SerializeField] private bool _isMenuOpen = false;
    private PlayerInputManager _playerInputManager;
    [SerializeField] private ControlMapPopup _controlMapPopup;
    public PlayerInput PlayerInput;

    [SerializeField] private GameObject _resumeButtonFirst;
    [SerializeField] private GameObject _settingMenuFirst;
    private bool isPaused;
    private void Start()
    {
        _mainMenu.SetActive(false);
        _settingMenu.SetActive(false);
    }

    private void Update()
    {
        if(InputManager.instance.MenuOpenInput)
        {
            if(!PauseManager.instance.IsPause)
            {
                Pause();
            }
        }

        else if (InputManager.instance.MenuCloseInput)
        {
            if(PauseManager.instance.IsPause)
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        PauseManager.instance.PauseGame();
        OpenMainMenu();
    }

    public void Unpause()
    {
        PauseManager.instance.UnpauseGame();
        CloseMainMenu();
    }

    private void OpenMainMenu()
    {
        _mainMenu.SetActive(true);
        _settingMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_resumeButtonFirst);
    }

    private void OpenSettingMenu()
    {
        _controlMapPopup.popupPanel.SetActive(true);
        _mainMenu.SetActive(false);

        // EventSystem.current.SetSelectedGameObject(_settingMenuFirst);
    }

    private void CloseMainMenu()
    {
        _mainMenu.SetActive(false);
        _settingMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnSettingPress()
    {
        OpenSettingMenu();
    }

    public void OnResumePress()
    {
        Unpause();
    }

    public void OnExitPress()
    {
        Application.Quit();
    }
}
