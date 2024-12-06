using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public SelectButtonHandler selectButtonHandler;
    private ButtonSelected buttonSelected;
    public GameObject settingGameObject;
    private bool exit = false;

    public GameObject MenuData;
    public GameObject MenuNoData;

    
    // Daftar tombol dan scene yang akan dimuat
    public SceneButton[] sceneButtons;
    private GameObject _selectedButton;
    public bool IsSetting { get; set; }
    private bool hasData = false;
    private SaveSystemJSON saveSystem;

    private void Awake() 
    {
        selectButtonHandler = gameObject.GetComponent<SelectButtonHandler>();
        buttonSelected = gameObject.GetComponent<ButtonSelected>();
        saveSystem = GameObject.FindObjectOfType<SaveSystemJSON>(); // Temukan SaveSystemJSON
    }
    void Start()
    {
        settingGameObject.SetActive(false);
        selectButtonHandler.FirstButton(buttonSelected);

        if (SaveSystemJSON.Instance.CheckData())
        {
            Debug.Log("ada data");
            MenuData.SetActive(true);
            MenuNoData.SetActive(false);
            hasData = true;
        }
        else
        {
            hasData = false;
            Debug.Log("gak ada");
            MenuData.SetActive(false);
            MenuNoData.SetActive(true);
        }
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
                    AudioManager.Instance.Play("ButtonClick");
                    if (sceneButton.isExitButton == true)
                    {
                        ExitApplication();
                        Debug.Log("exit");
                        exit = true;
                    }
                    else if (sceneButton.isSettingButton == true)
                    {
                        IsSetting = true;
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

        Debug.Log(IsSetting);

        if(hasData == true)
        {
            _selectedButton = sceneButtons[4].button.gameObject;
            EventSystem.current.SetSelectedGameObject(_selectedButton);
            hasData = false;
        }

        if(InputManager.instance.PauseInput && SettingsManager.instance.IsSetting == false)
        {
            settingGameObject.SetActive(false);
            _selectedButton = sceneButtons[0].button.gameObject;
            EventSystem.current.SetSelectedGameObject(_selectedButton);
            AudioManager.Instance.Play("OpenMenu");
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