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
    [SerializeField] private GameObject _zooKeeperMenu;
    [SerializeField] private GameObject _questMenu;

    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _settingButton;
    [SerializeField] private GameObject _backButton;
    
    private GameObject[] pauseButtons;
    
    [SerializeField] private bool _isPaused;
    
    [SerializeField] private bool _menuOpen = false;
    [SerializeField] private bool _openQuest = false;
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
        _zooKeeperMenu.SetActive(false);
        _questMenu.SetActive(false);
    }

    private void Update()
    {
        if(FindObjectOfType<MainMenu>()) return;
        
        if(InputManager.instance.PauseInput)
        {
            if(_menuOpen != true)
            {
                if(!PauseManager.instance.IsPause)
                {
                    Pause();
                    AudioManager.Instance.Play("OpenMenu");
                }
            }
        }
        
        if ((_menuOpen == true || _openQuest == true) && InputManager.instance.CloseMenu)
        {
            _menuOpen = false;
            _openQuest = false;
            CloseMenu();
        }
        
        if(_menuOpen == false && InputManager.instance.MenuInfo)
        {
            _menuOpen = true;
            OpenMenuInfo();
        } 

        if(InputManager.instance.MenuQuest && _openQuest == false)
        {
            _openQuest = true;
            // UIQuestLog.instance.FirstButton();
            OpenMenuQuest();
        }

        //  Debug.Log("hai" + UIQuestLog.instance.quest.Length + "" );

        // Debug.Log("Current action map: " + InputManager.PlayerInput.currentActionMap.name);
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

    public void OpenMenuInfo()
    {
        StatisticManager.instance.OpenMenu();
        _zooKeeperMenu.SetActive(true);
    }
    public void OpenMenuQuest()
    {
        QuestManager.instance.OpenMenu();
        _questMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        StatisticManager.instance.CloseMenu();
        QuestManager.instance.CloseMenu();
        _zooKeeperMenu.SetActive(false);
        _questMenu.SetActive(false);
    }
}
