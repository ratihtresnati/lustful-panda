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
    [SerializeField] private GameObject _controlMap;
    //[SerializeField] private bool _isMenuOpen = false;
    private PlayerInputManager _playerInputManager;
    public PlayerInput PlayerInput;

    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _settingButton;
    [SerializeField] private GameObject _backButton;
    
    private bool isPaused;
    private void Start()
    {
        _mainMenu.SetActive(false);
        _settingMenu.SetActive(false);
        _controlMap.SetActive(false);
    }

    private void Update()
    {
        if(InputManager.instance.PauseInput)
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

        if (InputManager.instance.ButtonClickInput)
        {
            GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

            if (selectedButton != null)
            {
                if (selectedButton == _settingButton)
                {
                    OnSettingPress();
                }
                else if (selectedButton == _resumeButton)
                {
                    OnResumePress();
                }
                else if (selectedButton == _backButton)
                {
                    OnExitPress();
                }
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

        EventSystem.current.SetSelectedGameObject(_resumeButton);
    }

    private void OpenSettingMenu()
    {
        _settingMenu.SetActive(true);
        _mainMenu.SetActive(false);

        SettingsManager.instance.FirstSelected();
        // EventSystem.current.SetSelectedGameObject(_settingMenuFirst);
    }

    private void CloseMainMenu()
    {
        _mainMenu.SetActive(false);
        _settingMenu.SetActive(false);
        _controlMap.SetActive(false);

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
