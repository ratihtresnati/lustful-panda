using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingMenu;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _settingButton;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _controlMap;
    GameObject _selectedButton;
    
    private GameObject[] pauseButtons;
    
    [SerializeField] private bool _isPaused;
    public bool IsMouse { get; set; }
    private Mouse _mouse;

    [Header("Animation UI")]
    public float scaleMultiplier = 1.1f;
    public float animationDuration = 0.2f;

    private void Awake()
    {
        _mainMenu = GameObject.Find("PauseMenu");
        _settingMenu = GameObject.Find("Settings Menu");
        _resumeButton = GameObject.Find("Resume");
        _settingButton = GameObject.Find("Settings");
        _backButton = GameObject.Find("Back");
        _controlMap = GameObject.Find("ControlMap UI");
        
        _mouse = FindObjectOfType<Mouse>();
    }

    private void Start()
    {
        _mainMenu.SetActive(false);
        _settingMenu.SetActive(false);
        _controlMap.SetActive(false);

        pauseButtons = new GameObject[] { _resumeButton, _settingButton, _backButton };
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

        else if (InputManager.instance.ResumeInput)
        {
            if(PauseManager.instance.IsPause)
            {
                if(_isPaused == true)
                {
                    OpenMainMenu();
                    _isPaused = false;
                }
                else
                {
                    Unpause();
                }
            }
        }
        
        _selectedButton = EventSystem.current.currentSelectedGameObject;

            if (_selectedButton != null)
            {
                if (_selectedButton == _settingButton)
                {     
                    OnPointerEnter(_settingButton);
                    if(InputManager.instance.ButtonClickInput)
                    {
                        OnSettingPress();
                    }
                }
                else
                {
                    OnPointerExit(_settingButton);
                }
                
                if (_selectedButton == _resumeButton)
                {
                    OnPointerEnter(_resumeButton);
                    if(InputManager.instance.ButtonClickInput)
                    {
                        OnResumePress();
                    }
                }
                else
                {
                    OnPointerExit(_resumeButton);
                }
                
                if (_selectedButton == _backButton)
                {
                    OnPointerEnter(_backButton);
                    if(InputManager.instance.ButtonClickInput)
                    {
                        OnBackPress();
                    }
                }
                else
                {
                    OnPointerExit(_backButton);
                }
            }

            MouseHover(_selectedButton);
        Debug.Log(IsMouse);
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

        EventSystem.current.SetSelectedGameObject(_resumeButton);
    }

    private void OpenSettingMenu()
    {
        _settingMenu.SetActive(true);
        _mainMenu.SetActive(false);

        SettingsManager.instance.FirstSelected();
        
        _isPaused = true;
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

    public void OnBackPress()
    { 
        PauseManager.instance.UnpauseGame();

        Loading.instance.LoadScene(0);
    }

    public void OnPointerEnter(GameObject gameObject)
    {
        if (gameObject == null) return;
        gameObject.transform.DOKill(); 
        gameObject.transform.DOScale(new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier), animationDuration).SetUpdate(true);
    }

    public void OnPointerExit(GameObject gameObject)
    { 
        gameObject.transform.DOKill(); 
        gameObject.transform.DOScale(Vector3.one, animationDuration).SetUpdate(true);
    }

    public GameObject[] ButtonPauseMenu()
    {
        return pauseButtons;
    }

    private void MouseHover(GameObject gameObject)
    {
        if (IsMouse == true) 
        {
            gameObject = _mouse.LastHoveredButton();
            EventSystem.current.SetSelectedGameObject(_mouse.LastHoveredButton());
        }
        else
        {
            gameObject = EventSystem.current.currentSelectedGameObject;
        }
    }
}
