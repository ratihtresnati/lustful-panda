// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class UserInput : MonoBehaviour
// {
//     public static UserInput instance;
//     public Vector2 MoveInput { get; private set; }
//     public bool RunInput { get; private set; }
//     public bool JumpInput { get; private set; }
//     public bool RollInput { get; private set; }
//     public bool InteractInput { get; private set; }
//     public bool PauseInput { get; private set; }
//     public static PlayerInput PlayerInput;
//     private InputAction _moveAction;
//     private InputAction _runAction;
//     private InputAction _jumpAction;
//     private InputAction _rollAction;
//     private InputAction _interactAction;
//     private InputAction _pauseAction;

//     private void Awake()
//     {
//         if(instance == null)
//         {
//             instance = this;
//         }
        
//         PlayerInput = gameObject.GetComponent<PlayerInput>();
//         SetupInputAction();
//     }

//     private void Update() {
//         UpdateInputs();
//     }
    
//     private void SetupInputAction(){
//         _moveAction = PlayerInput.actions["Move"];
//         _runAction = PlayerInput.actions["Run"];
//         _jumpAction = PlayerInput.actions["Jump"];
//         _rollAction = PlayerInput.actions["Roll"];
//         _interactAction = PlayerInput.actions["Interact"];
//         _pauseAction = PlayerInput.actions["Pause"];
//     }
//     private void UpdateInputs(){
//         MoveInput = _moveAction.ReadValue<Vector2>();
//         RunInput = _runAction.WasPressedThisFrame();
//         JumpInput = _jumpAction.WasPressedThisFrame();
//         RollInput = _rollAction.WasPressedThisFrame();
//         InteractInput = _interactAction.WasPressedThisFrame();
//         PauseInput = _pauseAction.WasPressedThisFrame();
//     }
// }
