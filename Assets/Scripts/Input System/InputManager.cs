using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static PlayerInput PlayerInput;
    public static PlayerInputManager PlayerInputManager;
    public bool PauseInput { get; private set; }
    public bool ResumeInput { get; private set; }
    public bool ButtonClickInput { get; private set; }
    public bool InteractClickInput { get; private set; }
    public bool InteractInputDown { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public bool RunPressed { get; private set; }
    public bool RunReleased { get; private set; }
    public bool JumpInput { get; private set; }
    public bool RollInput { get; private set; }
    public bool InteractInput { get; private set; }
    private InputAction _pauseAction;
    private InputAction _resumeAction;
    private InputAction _selectAction;
    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _jumpAction;
    private InputAction _rollAction;
    private InputAction _interactAction;
    
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PlayerInputManager = new PlayerInputManager(); 
        PlayerInput = gameObject.GetComponent<PlayerInput>();

        SetupInputAction();
    }
    private void Update() 
    {
        UpdateInputs();
    }
    
    private void SetupInputAction(){
        _resumeAction = PlayerInput.actions["Resume"];
        _pauseAction = PlayerInput.actions["Pause"];
        _selectAction = PlayerInput.actions["Click"];
        _interactAction = PlayerInput.actions["Interact"];
        _moveAction = PlayerInput.actions["Move"];
        _runAction = PlayerInput.actions["Run"];
        _jumpAction = PlayerInput.actions["Jump"];
        _rollAction = PlayerInput.actions["Roll"];
        
    }
    private void UpdateInputs(){
        PauseInput = _pauseAction.WasPressedThisFrame();
        ResumeInput = _resumeAction.WasPressedThisFrame();
        ButtonClickInput = _selectAction.WasPressedThisFrame();

        //interact
        InteractClickInput = _interactAction.WasPressedThisFrame(); //untuk button click
        InteractInputDown = _interactAction.IsPressed(); //untuk interact hold
        InteractInput = _interactAction.WasPressedThisFrame(); //untuk interact pressed

        MoveInput = _moveAction.ReadValue<Vector2>();
        RunPressed = _runAction.WasPressedThisFrame();
        RunReleased = _runAction.WasReleasedThisFrame();
        JumpInput = _jumpAction.WasPressedThisFrame();
        RollInput = _rollAction.WasPressedThisFrame();
        PauseInput = _pauseAction.WasPressedThisFrame();
    }

    private Vector2 _currentInputVector;
    private Vector2 _smoothInputVector;
    public float _smoothInputSpeed = 0.04f;

    public Vector2 GetMovementControl()
    {
        Vector2 inputVector = MoveInput;

        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, inputVector, ref _smoothInputVector, _smoothInputSpeed);
         
        inputVector = inputVector.normalized;

        return _currentInputVector;
    }
}
