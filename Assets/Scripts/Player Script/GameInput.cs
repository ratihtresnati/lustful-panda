using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnJumpingEvent;
    public event EventHandler OnRunningEvent;
    public event EventHandler OutRunningEvent;
    public event EventHandler OnRollingEvent;

    private PlayerInputManager playerInputManager;

    private void Awake()
    {

        playerInputManager = new PlayerInputManager();
        playerInputManager.Player.Enable();

        // Player Run Input System
        playerInputManager.Player.Run.performed += OnRun;
        playerInputManager.Player.Run.canceled += OutRun;

        // Player Jump Input System
        playerInputManager.Player.Jump.performed += Onjump;

        // Player Roll Input System
        playerInputManager.Player.Roll.performed += OnRoll;
   
    }

    private void Onjump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpingEvent(this, EventArgs.Empty);
    } 
    
    private void OnRoll(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRollingEvent(this, EventArgs.Empty);
    } 
    
    private void OnRun(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRunningEvent(this, EventArgs.Empty);
    }

    private void OutRun(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OutRunningEvent(this, EventArgs.Empty);
    } 

    public Vector2 GetMovementControl()
    {
        Vector2 inputVector = playerInputManager.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
