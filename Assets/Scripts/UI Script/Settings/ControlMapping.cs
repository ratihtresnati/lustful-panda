using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ControlMapping : MonoBehaviour
{
    public static ControlMapping instance;
    public GameObject[] Buttons;
    private void Update()
    {
        if (InputManager.instance.ButtonClickInput)
        {
            GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

            if (selectedButton != null && Buttons.Length >= 5)
            {
                Button BComponent = selectedButton.GetComponent<Button>();
                if (selectedButton == Buttons[0])
                {
                    BComponent.onClick.Invoke();
                }
                else if (selectedButton == Buttons[1])
                {
                    BComponent.onClick.Invoke();
                }
                else if (selectedButton == Buttons[2])
                {
                    BComponent.onClick.Invoke();
                }
                else if (selectedButton == Buttons[3])
                {
                    BComponent.onClick.Invoke();
                }
                else if (selectedButton == Buttons[4])
                {
                    BComponent.onClick.Invoke();
                }
                else if (selectedButton == Buttons[5])
                {
                    BComponent.onClick.Invoke();
                }
            }
        }
    }
}