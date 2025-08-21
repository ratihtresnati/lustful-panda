using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBlendTree : MonoBehaviour
{

    [SerializeField] private CharacterAnimatorControllerStateLama _characterAnimator;
    [SerializeField] private PlayerController _playerController;
    private float _animHorizontal;
    private float _animVertical;
    [SerializeField] private float _animeSmoothSpeed = 2;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimatorControllerStateLama>();
        _playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        AnimateWalkRun(new Vector3(_playerController.GetInputX(), _playerController.GetInputY(), 0));

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
    }
    private void AnimateWalkRun(Vector3 input) 
    {
        float multiplier = _playerController.IsRun ? 3 : 2f;
        float targetHorizontal = input.x * multiplier;
        float targetVertical = input.y * multiplier;

        _animHorizontal = Mathf.Lerp(_animHorizontal, targetHorizontal, Time.deltaTime * _animeSmoothSpeed);
        _animVertical = Mathf.Lerp(_animVertical, targetVertical, Time.deltaTime * _animeSmoothSpeed);

        _characterAnimator.WalkSpeed(_animHorizontal, _animVertical);
    }
}
