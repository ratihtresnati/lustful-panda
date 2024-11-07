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

            if (selectedButton != null)
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