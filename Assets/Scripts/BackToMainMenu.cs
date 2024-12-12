using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    // void Update()
    // {
    // }
    public void ButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
    }

    public void Restart()
    {
        Debug.Log("Restart");
        SaveSystemJSON.Instance.LoadGame();
        Time.timeScale = 1;
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
    }

    #region selectedbutton

    GameObject selectedButton;
    private SelectButtonHandler selectButtonHandler;
    private ButtonSelected buttonSelected;

    private void Awake()
    {
        selectButtonHandler = gameObject.GetComponent<SelectButtonHandler>();
        buttonSelected = gameObject.GetComponent<ButtonSelected>(); 
    }

    void Start()
    {
        FirstButton();
        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
    }

    private void Update()
    {
        selectButtonHandler.SelectButton();

        if(selectButtonHandler.SelectedButton != null)
        {
            selectedButton = selectButtonHandler.SelectedButton;
        }

        if (selectedButton != null)
        {
            if (selectedButton == selectButtonHandler.SelectedButton.gameObject)
            {
                if(InputManager.instance.ButtonClickInput)
                {
                    AudioManager.Instance.Play("ButtonClick");
                    if (selectedButton.gameObject.name == "Button_Restart")
                    {    
                        Restart();
                    }
                    
                    if (selectedButton.gameObject.name == "Button_Back") 
                    {
                        ButtonPressed();
                    }
                }
            }
        }      
    }

    public void FirstButton()
    {
        selectButtonHandler.FirstButton(buttonSelected);
    }

    #endregion
}
