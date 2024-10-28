using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float _jumpSpeed = 3f;
    [SerializeField]
    private float _walkSpeed = 3f;
    [SerializeField]
    private float _runSpeed = 6f;
    [SerializeField]
    private float _rotationSpeed = 90f;
    [SerializeField]
    AnimationCurve _rollCurve;

    private CharacterController characterController;
    private float ySpeed;
    private bool _isRun;

    private bool isRooling;
    private float _rollTimer;
    private float _speed;
    private float hInput;
    private float vInput;
    private Vector3 move;
    private Vector3 velocity;

    [Header("Animasi")]
    public CharacterAnimatorController CharacterAnimatorController;
    // public AnimationLookAt AnimationLookAt;
    [SerializeField] private float _animeSmoothSpeed = 2;
    [SerializeField] private float _animHorizontal, _animVertical;
    private JumpState _currentState = JumpState.Grounded;
    private bool _isJump = false;
    [SerializeField] private float _jumpDelayDuration = 2f;
    private bool _isTurnRight;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        CharacterAnimatorController = GetComponent<CharacterAnimatorController>();

        Keyframe roll_lastFrame = _rollCurve[_rollCurve.length - 1];
        _rollTimer = roll_lastFrame.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRooling)
        {
        gameObject.tag = "PandaMC";
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        _speed = _walkSpeed;

        move = new Vector3(hInput, 0, vInput);

        //walk & run
        _isRun = Input.GetKey(KeyCode.LeftShift);
        _speed = _isRun ? _runSpeed : _walkSpeed; 
        
        if (!Input.GetKey(KeyCode.LeftShift) && _isRun)
        {
            _isRun = false;
        }

        float magnitude = Mathf.Clamp01(move.magnitude) * _speed;
        move.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;


        velocity = move * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);
        if (move != Vector3.zero)
        {
            Vector3 desiredDirection = new Vector3(hInput, 0, vInput);
            if (desiredDirection.magnitude > 0) 
            {
                desiredDirection.Normalize();
            }
            float angleDiff = Vector3.Angle(transform.forward, desiredDirection);

            float speedModifier = Mathf.Lerp(1f, 0.1f, Mathf.InverseLerp(45f, 135f, angleDiff));
            _speed *= speedModifier;

            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
          
          
        }

            //Jump
            if (characterController.isGrounded)
            {
                ySpeed = 0;
                if (Input.GetKeyDown(KeyCode.Space)) 
                { 
                    StartCoroutine(Jumping());
                    _currentState = JumpState.Jump;
                }
            }

            //Rolling
            if (!_isJump) { 
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (velocity.magnitude != 0) StartCoroutine(Rolling());
                }
            }

        }

        //animasi
        AnimateWalkRun(new Vector3(hInput, vInput, 0));
        AnimateJump();
        AnimateRest();
        AnimateSit();

        if(Input.GetKeyDown(KeyCode.I))
        {
            StopAnimation();
        }
    }

    #region Rolling
    IEnumerator Rolling()
    {
        //selagi jump dia gak bisa roll
        if (_isJump == true) 
        { 
            yield return null;
        }

        isRooling = true;
        gameObject.tag = "PandaRolling";
        CharacterAnimatorController.Roll();
        float timer = 0;
        while (timer < _rollTimer) {
            float _rollSpeed = _rollCurve.Evaluate(timer);
            Vector3 dir = (transform.forward * _rollSpeed);
            characterController.Move(dir * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        CharacterAnimatorController.StopRoll();
        isRooling = false;
    }

    #endregion
    
    #region Jumping
    IEnumerator Jumping()
    {
        _isJump = true;
        //pake delay, biar animasi jump jalan dulu sebelum character jump 
        yield return new WaitForSeconds(_jumpDelayDuration);
        
        ySpeed = _jumpSpeed;
        while (ySpeed != 0) {
            _isJump = true;

            yield return null;
        }
        _isJump = false;
    }
    #endregion

    #region animation
    //animasi jump
    private void AnimateJump()
    {
        switch (_currentState)
        {
            case JumpState.Grounded:
                break;
            case JumpState.Jump:
                CharacterAnimatorController.Jump();
                _currentState = JumpState.Falling;
                break;
            case JumpState.Falling:
                CharacterAnimatorController.Land();
                _currentState = JumpState.Grounded;
                break;
        }
    }

    //animasi walk & run
    private void AnimateWalkRun(Vector3 input) 
    {
        float multiplier = Input.GetKey(KeyCode.LeftShift) ? 3 : 2f;
        float targetHorizontal = input.x * multiplier;
        float targetVertical = input.y * multiplier;

        _animHorizontal = Mathf.Lerp(_animHorizontal, targetHorizontal, Time.deltaTime * _animeSmoothSpeed);
        _animVertical = Mathf.Lerp(_animVertical, targetVertical, Time.deltaTime * _animeSmoothSpeed);

        CharacterAnimatorController.WalkSpeed(_animHorizontal, _animVertical);
    }

    //animasi sit
    private void AnimateSit()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            CharacterAnimatorController.Sit();
        }
    }

    //animasi rest
    private void AnimateRest()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            CharacterAnimatorController.Rest();
        }
    }

    private void StopAnimation()
    {
        CharacterAnimatorController.UpSit();
        CharacterAnimatorController.UpRest();
    }

    public Vector3 PlayerControlMovement()
    {
        return move;
    }
#endregion
}

public enum JumpState
{
    Grounded,
    Jump,
    Falling
}
