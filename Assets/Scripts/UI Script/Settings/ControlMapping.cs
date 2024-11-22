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
    private void Update()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        if (selectedButton != previousSelectedButton)
        {
            if (previousSelectedButton != null)
            {
                FindObjectOfType<MenuManager>().OnPointerExit(previousSelectedButton);
            }

            FindObjectOfType<MenuManager>().OnPointerEnter(selectedButton);
            previousSelectedButton = selectedButton;
        }

        if (selectedButton != null)
        {
            if (InputManager.instance.ButtonClickInput && SettingsManager.instance.IsSetting == true)
            {
                EventSystem.current.SetSelectedGameObject(SettingsManager.instance._firstButtonCM);
                
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