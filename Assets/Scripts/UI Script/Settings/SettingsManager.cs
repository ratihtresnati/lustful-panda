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
