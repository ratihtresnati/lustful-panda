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
    public bool InteractClickInput { get; private set; }
    public bool MainMenu { get; private set; }
    public bool _interact { get; private set; }
    private InputAction _menuOpenAction;
    private InputAction _menuCloseAction;
    private InputAction _selectAction;
    private InputAction _interactAction;
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
        _interactAction = PlayerInput.actions["Interact"];
    }

    private void Update()
    {
        MenuOpenInput = _menuOpenAction.WasPressedThisFrame();
        MenuCloseInput = _menuCloseAction.WasPressedThisFrame();
        ButtonClickInput = _selectAction.WasPressedThisFrame();
        InteractClickInput = _interactAction.WasPressedThisFrame();

        // if (ButtonClickInput && _interact == false)
        // {
        //     InteractUI();
        // }
    }

    public void InteractUI ()
    {
        _interact = true;
        StartCoroutine(ResetInteract());
    }

    private IEnumerator ResetInteract()
    {
        yield return new WaitForSeconds(5f);
        _interact = false;
    }
}
