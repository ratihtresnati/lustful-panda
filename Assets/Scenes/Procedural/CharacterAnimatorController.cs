using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerInteract _playerInteract;
    [SerializeField] private float _jumpAnimationDuration = 0.6f;
    [SerializeField] private float _rollAnimationDuration = 0.8f;
    [SerializeField] private float _sitAnimationDuration = 0.5f;
    [SerializeField] private float _restAnimationDuration = 0.5f;
    private float _lockedTill;
    private bool _landed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInteract = GetComponent<PlayerInteract>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {

        if (_playerController.IsCatch)
        {
            StartCoroutine(Catching());
        }

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

        if (InputManager.instance.IsAction && _playerController.Move == Vector3.zero )
        {
            if (InputManager.instance.RestInput == true) return Rest;
            if (InputManager.instance.SitInput == true) return Sit;
        }

        if (_currentState == Rest && !InputManager.instance.RestInput) return LockState(UpRest, _restAnimationDuration);
        if (_currentState == Sit && !InputManager.instance.SitInput) return LockState(UpSit, _sitAnimationDuration);

       
        if (_playerController.isGrounded == true)
        {
            if (_playerController.IsRooling == true) return LockState(Roll, _rollAnimationDuration);
            if (_playerController.IsRun == true && _playerController.Move != Vector3.zero) return Run;
            if (_playerInteract.PushBox == true) return WalkSlow;

            return _playerController.Move == Vector3.zero ? Idle : Walk;
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
    private static readonly int WalkSlow = Animator.StringToHash("WalkSlow");
    private static readonly int Rest = Animator.StringToHash("Rest");
    private static readonly int UpRest = Animator.StringToHash("Up Rest");
    private static readonly int Sit = Animator.StringToHash("Sit");
    private static readonly int UpSit = Animator.StringToHash("Up Sit");

    IEnumerator Catching()
    {
        yield return new WaitForSeconds(0.8f);

        _animator.SetBool("isCatch", true);
    }
}
