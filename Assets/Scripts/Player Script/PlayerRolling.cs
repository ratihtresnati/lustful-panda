using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRolling : MonoBehaviour
{
    private CharacterAnimatorController _characterAnimatorController;

    void Start()
    {
        _characterAnimatorController = GetComponent<CharacterAnimatorController>();
    }
    void Update()
    {
        if(InputManager.instance.RollInput)
        {
            _characterAnimatorController.Roll();
        }
        else
        {
            _characterAnimatorController.StopRoll();
        }
        
    }
}
