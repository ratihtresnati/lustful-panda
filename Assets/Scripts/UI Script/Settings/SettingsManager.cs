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
    [SerializeField] private GameObject _settingMenu;
    [SerializeField] private GameObject _controlMapMenu;
    [SerializeField] private GameObject _firstButton;

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
                if (selectedButton == _controlMapBtn && InputManager.instance.ButtonClickInput)
                {
                    OpenControlMap();
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
        _settingMenu.SetActive(false);
        _controlMapMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_firstButton);
    }


    // [SerializeField] private Image[] navImage;

    // [SerializeField] private Sprite navClose, navOpen;

    // void Start()
    // {
    //     ShowNav(0);
    // }

    // public void ShowNav(int tutorialNum)
    // {

    //     foreach (var item in navImage)
    //     {
    //         item.sprite = navClose;
    //     } 
        
    //    if (tutorialNum == tutorialSO.Count)
    //     {
    //         tutorialNum = 0;
    //     }

    //     navImage[tutorialNum].sprite = navOpen;
    // }

    // public void SwitchTab(int tabNo)
    // {
    //     foreach (GameObject tab in Tabs)
    //     {
    //         tab.SetActive(false);
    //     }

        // Tabs[tabNo].SetActive(true);

        // foreach (Image image in buttonImage)
        // {
        //     image.sprite 
        // }
    // }

    
}
