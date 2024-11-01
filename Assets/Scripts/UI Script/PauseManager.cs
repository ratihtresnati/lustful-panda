using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public interactItem interactItems;
    public PandaQuest pandaQuest;
    public conditionalObjectInteract conditionalObjectInteract;

    public bool IsPause { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        interactItems = FindObjectOfType<interactItem>();
        conditionalObjectInteract = FindObjectOfType<conditionalObjectInteract>();
        pandaQuest = FindObjectOfType<PandaQuest>();
    }

    public void PauseGame()
    {
        IsPause = true;
        Time.timeScale = 0f;

        InputManager.PlayerInput.SwitchCurrentActionMap("UI");

        interactItems.enabled = false;
        pandaQuest.enabled = false;
        conditionalObjectInteract.enabled = false;

        // InputManager.PlayerInputManager.Player.Disable();
        // InputManager.PlayerInputManager.UI.Enable();
    }

    public void UnpauseGame()
    {
        IsPause = false;
        Time.timeScale = 1f;

        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
        // InputManager.PlayerInputManager.Player.Enable();
        // InputManager.PlayerInputManager.UI.Disable();
    }
}
