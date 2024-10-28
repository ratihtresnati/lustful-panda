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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            _characterAnimatorController.Roll();
        }
        else
        {
            _characterAnimatorController.StopRoll();
        }
        
    }
}
