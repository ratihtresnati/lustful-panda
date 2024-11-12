using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    public GameObject[] Tabs;
    public GameObject[] Buttons;
    [SerializeField] private GameObject _controlMapBtn;
    [SerializeField] private GameObject _controlDisplayBtn;
    [SerializeField] private GameObject _controlMapMenu;
    [SerializeField] private GameObject _controlDisplayMenu;
    [SerializeField] private GameObject _firstButtonCM;
    [SerializeField] private GameObject _firstButtonCD;

    private bool _isControlMap = false;
    private bool _isControlDisplay = false;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        CloseAllTabs();
    }

    private void Update()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

            if (selectedButton != null)
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    Tabs[i].SetActive(selectedButton == Buttons[i]);
                }
                
                if (selectedButton == _controlMapBtn && InputManager.instance.ButtonClickInput && _isControlMap == false)
                {
                    OpenControlMap();
                    
                }else if (selectedButton == _controlMapBtn && _isControlMap == true)
                {
                    CloseControlMap();
                }
                if (selectedButton == _controlDisplayBtn && InputManager.instance.ButtonClickInput && _isControlDisplay == false)
                {
                    OpenControlDisplay();
                }
                else if (selectedButton == _controlDisplayBtn && _isControlDisplay == true)
                {
                    CloseControlDisplay();
                }
            }
    }

    private void CloseAllTabs()
    {
        foreach (GameObject tab in Tabs)
        {
            tab.SetActive(false);
        }
    }

    public void FirstSelected()
    {
        EventSystem.current.SetSelectedGameObject(Buttons[0]);
    }

    private void OpenControlMap(){
        _controlMapMenu.SetActive(true);
        _isControlMap = true;

        EventSystem.current.SetSelectedGameObject(_firstButtonCM);
    }
    private void OpenControlDisplay(){
        _controlDisplayMenu.SetActive(true);
        _isControlDisplay = true;

        EventSystem.current.SetSelectedGameObject(_firstButtonCD);
    }
    private void CloseControlMap(){
        _controlMapMenu.SetActive(false);
        _isControlMap = false;
    }
    private void CloseControlDisplay(){
        _controlDisplayMenu.SetActive(false);
        _isControlDisplay = false;
    }
}
