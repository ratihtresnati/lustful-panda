using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatisticManager : MonoBehaviour
{
    public static StatisticManager instance;
    public ButtonZooKeeper[] npc;
    [SerializeField] private GameObject _statisticCanvas;
    // private int _num;
    private bool _isPause = false;

    GameObject selectedButton;
    private SelectButtonHandler selectButtonHandler;
    private ButtonSelected buttonSelected;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

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

        foreach (var buttonSetting in npc)
        {
            if (selectedButton == buttonSetting.button.gameObject)
            {
                int index = System.Array.IndexOf(npc, buttonSetting);
                SelectButton(index);
                Debug.Log(index);
                break;
            }
        }
    }

    public void SelectButton(int index)
    {
        selectedButton = npc[index].button.gameObject;
        EventSystem.current.SetSelectedGameObject(npc[index].button.gameObject);

        for (int i = 0; i < npc.Length; i++)
        {
            // if (i == index)
            // {
            //     // npc[i].ShowData();
            // }

            if (i == index)
            {
                npc[i].Button(true);
                npc[i].ShowData();
            }
            else
            {
                npc[i].Button(false);
            }
        }
    }

    public void FirstButton()
    {
        SelectButton(0);
    }

    public void OpenMenu()
    {
        FirstButton();
        _isPause = true;
        Time.timeScale = 0f;
        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void CloseMenu()
    {
        _isPause = false;
        Time.timeScale = 1f;
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
        EventSystem.current.SetSelectedGameObject(null);
    }

}