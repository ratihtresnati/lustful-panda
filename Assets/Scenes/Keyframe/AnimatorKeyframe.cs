using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorKeyframe : MonoBehaviour
{
    [SerializeField] private CharacterAnimatorControllerStateLama _characterAnimator;

    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimatorControllerStateLama>();
        _playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        // Idle / Walk / Run
        if (_playerController.Move.magnitude > 0.1f && _playerController.isGrounded && !_playerController.IsRooling && !_playerController.IsJump)
        {
            if (_playerController.IsRun)
            {
                _characterAnimator.Running();
            }
            else
            {
                _characterAnimator.Walk();
            }
        }

        if (InputManager.instance.IsAction)
        {
            _characterAnimator.Idle();
        }

        // Jump
        if (_playerController.IsJump)
        {
            _characterAnimator.Jump();
        }
        else
        {
            _characterAnimator.Land();
        }

        // Roll
        if (_playerController.IsRooling)
        {
            _characterAnimator.Roll();
        }
        else
        {
            _characterAnimator.StopRoll();
        }

        // Rest
        if (InputManager.instance.RestInput)
        {
            _characterAnimator.Rest();
        }
        else
        {
            _characterAnimator.UpRest();
        }

        // Sit
        if (InputManager.instance.SitInput)
        {
            _characterAnimator.Sit();
        }
        else
        {
            _characterAnimator.UpSit();
        }

        if (_playerController.IsTurnLeft)
        {
            _characterAnimator.Left();
        }

        if (_playerController.IsTurnRight)
        {
            _characterAnimator.Right();
        }
        
        if (!_playerController.IsTurnLeft && !_playerController.IsTurnRight)
        {
            _characterAnimator.Noturn();
        }

    }
}
