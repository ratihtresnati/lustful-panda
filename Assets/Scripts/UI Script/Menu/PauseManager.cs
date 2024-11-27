using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public SelectButtonHandler selectButtonHandler;
    public ButtonSelected buttonSelected;
    public static PauseManager instance;
    public interactItem interactItems;
    public PandaQuest pandaQuest;
    private GameObject _selectedButton;
    
    [SerializeField] private GameObject _settingMenu;
    [SerializeField] private GameObject _mainMenu;


    public bool IsPause { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        pandaQuest = FindObjectOfType<PandaQuest>();

        selectButtonHandler = gameObject.GetComponent<SelectButtonHandler>();
        buttonSelected = gameObject.GetComponent<ButtonSelected>();

        _mainMenu = GameObject.Find("PauseMenu");
        _settingMenu = GameObject.Find("Settings Menu");
    }

    public void Start()
    {
        _settingMenu.SetActive(false);
    }

    private void Update()
    {
        selectButtonHandler.SelectButton();

        if(selectButtonHandler.SelectedButton != null)
        {
            _selectedButton = selectButtonHandler.SelectedButton;
        }

        if (_selectedButton != null)
        {
            if (_selectedButton == selectButtonHandler.SelectedButton.gameObject)
            {
                if(InputManager.instance.ButtonClickInput)
                {
                    if (_selectedButton.gameObject.name == "Settings Button")
                    {    
                        OnSettingPress();
                        AudioManager.Instance.Play("ButtonClick");
                    }
                    
                    if (_selectedButton.gameObject.name == "Resume Button") 
                    {
                        OnResumePress();
                        AudioManager.Instance.Play("ButtonClick");
                    }
                        
                    if (_selectedButton.gameObject.name == "Back Button")
                    {
                        OnBackPress();
                        AudioManager.Instance.Play("ButtonClick");
                    }
                }
            }
        }      
    }

    public void PauseGame()
    {
        selectButtonHandler.FirstButton(buttonSelected);

        IsPause = true;
        Time.timeScale = 0f;

        InputManager.PlayerInput.SwitchCurrentActionMap("UI");

        pandaQuest.enabled = false;
    }

    public void UnpauseGame()
    {
        IsPause = false;
        Time.timeScale = 1f;

        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
    }
    private void OpenSettingMenu()
    {
        Debug.Log("hai");
        selectButtonHandler.OpenPage(0, _mainMenu, _settingMenu);

        SettingsManager.instance.FirstSelected();
        
        // _isPaused = true;
    }

    public void CloseSettingMenu()
    {
        selectButtonHandler.OpenPage(0, _settingMenu, _mainMenu);
        selectButtonHandler.FirstButton(buttonSelected);
    }

    public void OnSettingPress()
    {
        OpenSettingMenu();
    }

    public void OnResumePress()
    {
        Debug.Log("resume");
        selectButtonHandler.OpenPage(0, _mainMenu);
        UnpauseGame();
    }

    public void OnBackPress()
    { 
        selectButtonHandler.LoadScene(0);
        UnpauseGame();
    }
}
