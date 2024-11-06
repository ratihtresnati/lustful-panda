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

    public PlayerInputManager PlayerInputManager;
    private Vector2 _currentInputVector;
    private Vector2 _smoothInputVector;
    public float _smoothInputSpeed = 0.2f;

    private void Awake()
    {

        PlayerInputManager = new PlayerInputManager();
        PlayerInputManager.Player.Enable();

        // Player Run Input System
        PlayerInputManager.Player.Run.performed += OnRun;
        PlayerInputManager.Player.Run.canceled += OutRun;

        // Player Jump Input System
        PlayerInputManager.Player.Jump.performed += Onjump;

        // Player Roll Input System
        PlayerInputManager.Player.Roll.performed += OnRoll;
   
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
        Vector2 inputVector = PlayerInputManager.Player.Move.ReadValue<Vector2>();

        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, inputVector, ref _smoothInputVector, _smoothInputSpeed);
         
        inputVector = inputVector.normalized;
        
        return _currentInputVector;
    }
}
