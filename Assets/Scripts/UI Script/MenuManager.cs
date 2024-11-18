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
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _settingButton;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _controlMap;
    [SerializeField] private GameObject _controlDisplay;
    [SerializeField] private GameObject _controlAudio;
    
    private bool _isPaused;

    AudioManager audioManager;

    private void Awake()
    {
        _mainMenu = GameObject.Find("PauseMenu");
        _settingMenu = GameObject.Find("Settings Menu");
        _resumeButton = GameObject.Find("Resume");
        _settingButton = GameObject.Find("Settings");
        _backButton = GameObject.Find("Back");
        _controlMap = GameObject.Find("ControlMap UI");
        _controlDisplay = GameObject.Find("ControlDisplay UI");
        _controlAudio = GameObject.Find("ControlAudio UI");

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        _mainMenu.SetActive(false);
        _settingMenu.SetActive(false);
        _controlMap.SetActive(false);
        _controlDisplay.SetActive(false);
        _controlAudio.SetActive(false);
    }

    private void Update()
    {
        if(InputManager.instance.PauseInput)
        {
            if(!PauseManager.instance.IsPause)
            {
                Pause();
                audioManager.PlaySFX(audioManager.SFXButtonClick);
            }
        }

        else if (InputManager.instance.ResumeInput)
        {
            if(PauseManager.instance.IsPause)
            {
                if(_isPaused == true)
                {
                    OpenMainMenu();
                    _isPaused = false;
                    SettingsManager.instance._isControlAudio = false;
                    audioManager.PlaySFX(audioManager.SFXButtonClick);
                }
                else
                {
                    Unpause();
                    audioManager.PlaySFX(audioManager.SFXButtonClick);
                }
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
                    audioManager.PlaySFX(audioManager.SFXButtonClick);
                }
                else if (selectedButton == _resumeButton)
                {
                    OnResumePress();
                    audioManager.PlaySFX(audioManager.SFXButtonClick);
                }
                else if (selectedButton == _backButton)
                {
                    OnBackPress();
                    audioManager.PlaySFX(audioManager.SFXButtonClick);
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
        _settingMenu.SetActive(false);
        _controlMap.SetActive(false);
        _controlDisplay.SetActive(false);
        _controlAudio.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_resumeButton);
    }

    private void OpenSettingMenu()
    {
        _settingMenu.SetActive(true);
        _mainMenu.SetActive(false);

        SettingsManager.instance.FirstSelected();
        
        _isPaused = true;
        // EventSystem.current.SetSelectedGameObject(_settingMenuFirst);
    }

    private void CloseMainMenu()
    {
        _mainMenu.SetActive(false);
        _settingMenu.SetActive(false);
        _controlMap.SetActive(false);
        _controlDisplay.SetActive(false);
        _controlAudio.SetActive(false);

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

    public void OnBackPress()
    { 
        PauseManager.instance.UnpauseGame();
        // InputManager.PlayerInput.enabled = false;
        Loading.instance.LoadScene(0);
    }
}
