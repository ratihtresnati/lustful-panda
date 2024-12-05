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
        _resumeButton = GameObject.Find("Resume Button");
        _settingButton = GameObject.Find("Settings Button");
        _backButton = GameObject.Find("Back Button");
    }

    private void Start()
    {
        _mainMenu.SetActive(false);
        _settingMenu.SetActive(false);
    }

    private void Update()
    {
        if(FindObjectOfType<MainMenu>()) return;
        
        if(InputManager.instance.PauseInput)
        {
            if(!PauseManager.instance.IsPause)
            {
                Pause();
                AudioManager.Instance.Play("OpenMenu");
            }
        }
    }

    public void Pause()
    {
        PauseManager.instance.PauseGame();
        OpenMainMenu();
    }

    private void OpenMainMenu()
    {
        _mainMenu.SetActive(true);
        _settingMenu.SetActive(false);
    }
}
