using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static PlayerInput PlayerInput;
    public static PlayerInputManager PlayerInputManager;
    public bool MenuOpenInput { get; private set; }
    public bool MenuCloseInput { get; private set; }
    public bool ButtonClickInput { get; private set; }
    private InputAction _menuOpenAction;
    private InputAction _menuCloseAction;
    private InputAction _selectAction;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PlayerInputManager = new PlayerInputManager(); 
        PlayerInput = gameObject.GetComponent<PlayerInput>();
        _menuOpenAction = PlayerInput.actions["MenuOpen"];
        _menuCloseAction = PlayerInput.actions["MenuClose"];
        _selectAction = PlayerInput.actions["Click"];
    }

    private void Update()
    {
        MenuOpenInput = _menuOpenAction.WasPressedThisFrame();
        MenuCloseInput = _menuCloseAction.WasPressedThisFrame();
        ButtonClickInput = _selectAction.WasPressedThisFrame();
    }
}
