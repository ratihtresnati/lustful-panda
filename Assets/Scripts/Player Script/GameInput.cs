using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInput : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = new PlayerInputManager();
        playerInputManager.Player.Enable();
    }

    public Vector2 GetMovementControl()
    {
        Vector2 inputVector = playerInputManager.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
