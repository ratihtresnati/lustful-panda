using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ControlMapping : MonoBehaviour
{
    public static ControlMapping instance;
    public GameObject[] Buttons;
    private GameObject previousSelectedButton;
    private GameObject selectedButton;

    public  SelectButtonHandler selectButtonHandler;
    public ButtonSelected buttonSelected;

    private void Start()
    {
        selectButtonHandler = gameObject.GetComponent<SelectButtonHandler>();
        buttonSelected = gameObject.GetComponent<ButtonSelected>();
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
            if (InputManager.instance.ButtonClickInput && SettingsManager.instance.IsSetting == true)
            {
                int index = System.Array.IndexOf(Buttons, selectedButton);
                if (index >= 0 && index < Buttons.Length)
                {
                    Button BComponent = selectedButton.GetComponent<Button>();
                    BComponent?.onClick.Invoke();
                }
            }
        }
    }
}