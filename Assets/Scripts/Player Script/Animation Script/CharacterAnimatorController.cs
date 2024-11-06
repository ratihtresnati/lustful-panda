using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _jumpAnimationDuration = 0.1f;
    [SerializeField] private float _rollAnimationDuration = 0.1f;
    private float _lockedTill;
    private bool _landed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        var state = GetState();

        if(state == _currentState) return;
        _animator.CrossFade(state, 0.2f, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if(Time.time < _lockedTill) return _currentState;

        if (_playerController.IsJump == true) 
        {
            _landed = true;
            return Jump;
        }
        if (_landed == true)
        {
            _landed = false;
            return LockState(Land, _jumpAnimationDuration);
        }
       
        if (_playerController.isGrounded == true) 
        {
            if (_playerController.IsRooling == true) return LockState(Roll, _rollAnimationDuration);
            if (_playerController.IsRun == true && _playerController.move != Vector3.zero) return Run;

            return _playerController.move == Vector3.zero ? Idle : Walk;
        }

        return Idle;
    }

    private int LockState(int state, float time)
    {
        _lockedTill = Time.time + time;
        return state;
    }

    private int _currentState;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Roll = Animator.StringToHash("Rolling");
    private static readonly int Jump = Animator.StringToHash("JumpStart");
    private static readonly int Land = Animator.StringToHash("Land");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Walk = Animator.StringToHash("Walking");
}
