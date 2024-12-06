using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    private SelectButtonHandler selectButtonHandler;
    private ButtonSelected buttonSelected;
    public GameObject settingGameObject;
    private bool exit = false;

    
    // Daftar tombol dan scene yang akan dimuat
    public SceneButton[] sceneButtons;
    private GameObject _selectedButton;
    private bool _isSetting;
    public bool IsMouse { get; set; }

    AudioManager audioManager;

    private void Awake() 
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        selectButtonHandler = gameObject.GetComponent<SelectButtonHandler>();
        buttonSelected = gameObject.GetComponent<ButtonSelected>();
    }
    void Start()
    {
        settingGameObject.SetActive(false);
        selectButtonHandler.FirstButton(buttonSelected);
    }

    private void Update()
    {
        selectButtonHandler.SelectButton();

        if(selectButtonHandler.SelectedButton != null)
        {
            _selectedButton = selectButtonHandler.SelectedButton;
        }

        foreach (SceneButton sceneButton in sceneButtons)
        {
            int index = sceneButton.sceneIndex; 
            if (_selectedButton == sceneButton.button.gameObject)
            {
                if (InputManager.instance.ButtonClickInput)
                {   
                    audioManager.Play("ButtonClick");
                    if (sceneButton.isExitButton == true)
                    {
                        ExitApplication();
                        Debug.Log("exit");
                        exit = true;
                    }
                    else if (sceneButton.isSettingButton == true)
                    {
                        _isSetting = true;
                        settingGameObject.SetActive(true);
                        SettingsManager.instance.FirstSelected();
                    }
                    else
                    {
                        selectButtonHandler.LoadScene(index);
                        Debug.Log("load");
                    }
                }
            }
        }

        // Debug.Log(_selectedButton );


        if(InputManager.instance.PauseInput && _isSetting == true)
        {
            settingGameObject.SetActive(false);
            _selectedButton = sceneButtons[0].button.gameObject;
        }
    }

    // Fungsi untuk keluar dari aplikasi
    public void ExitApplication()
    {
        Application.Quit();
        Debug.Log("Application has been exited."); // Hanya berfungsi di editor atau build yang didukung
    }

    public void FirstButton()
    {
        selectButtonHandler.FirstButton(buttonSelected);
    }
}