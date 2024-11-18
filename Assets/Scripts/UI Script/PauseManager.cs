using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public interactItem interactItems;
    public PandaQuest pandaQuest;

    public bool IsPause { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        pandaQuest = FindObjectOfType<PandaQuest>();
    }

    public void PauseGame()
    {
        IsPause = true;
        Time.timeScale = 0f;

        InputManager.PlayerInput.SwitchCurrentActionMap("UI");

        pandaQuest.enabled = false;
    }

    public void UnpauseGame()
    {
        IsPause = false;
        Time.timeScale = 1f;

        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
    }
}
