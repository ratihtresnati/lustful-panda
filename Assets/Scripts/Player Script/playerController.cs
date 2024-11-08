using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
// using System.Xml.Serialization;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _jumpSpeed = 3f;
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private float _rotationSpeed = 90f;
    private float _rollTimer;
    private float _speed;
    private float _ySpeed;
    [SerializeField] private float _jumpDelayDuration = 0.2f;
    [SerializeField] AnimationCurve _rollCurve;
    private CharacterController _characterController;
    
    [SerializeField]
    private Vector2 _inputVector;
    private Vector3 _velocity;
    public Vector3 Move { get; private set; }
    public bool IsJump { get; private set; }
    public bool IsRun { get; private set; }
    public bool IsRooling { get; private set; }
    public bool isGrounded { get; private set; }

    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        Keyframe roll_lastFrame = _rollCurve[_rollCurve.length - 1];
        _rollTimer = roll_lastFrame.time;

        // gameInput.OnRunningEvent += OnRunEvent;
        // gameInput.OutRunningEvent += OutRunEvent;
        // gameInput.OnJumpingEvent += OnJumpEvent;
        // gameInput.OnRollingEvent += OnRollEvent;
    }
   
    void Update()
    {
        HanddleMovements();

        //moveset
        if(InputManager.instance.JumpInput){
            if (!IsRooling){
                if (_characterController.isGrounded && !IsJump){
                    StartCoroutine(Jumping());
                }
            }
        }
        if(InputManager.instance.RollInput){
            if (!IsRooling){ 
                if (!IsJump){
                    if (_velocity.magnitude != 0) StartCoroutine(Rolling());
                }
            }
        }
        if(InputManager.instance.RunPressed){
            IsRun = true;
        }
        if(InputManager.instance.RunReleased){
            IsRun = false;
        }

        //Debug.Log(ySpeed);
        // Debug.Log(_IsJump);
    }

      private void HanddleMovements()
    {
        if (!IsRooling)
        {
        gameObject.tag = "PandaMC";

        //_inputVector = gameInput.GetMovementControl();
        _inputVector = InputManager.instance.MoveInput;

        Move = new Vector3(_inputVector.x, 0, _inputVector.y);

        if (IsRun)
        {
            _speed = _runSpeed;
        }
        else
        {
            IsRun = false;
            _speed = _walkSpeed;
        }

        float magnitude = Mathf.Clamp01(Move.magnitude) * _speed;
        Move.Normalize();

        _ySpeed += Physics.gravity.y * Time.deltaTime;

        _velocity = Move * magnitude;

        _velocity.y = _ySpeed;

        _characterController.Move(_velocity * Time.deltaTime);

        if (Move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Move, Vector3.up);

            float rotationStep = _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationStep);
        }

            if (_characterController.isGrounded)
            {
                isGrounded = true;
                _ySpeed = 0;
            }
        }
    }

    IEnumerator Rolling()
    {
        //selagi jump dia gak bisa roll
        if (IsJump == true) 
        { 
            yield return null;
        }

        IsRooling = true;
        gameObject.tag = "PandaRolling";
        float timer = 0;
        while (timer < _rollTimer) {
            float _rollSpeed = _rollCurve.Evaluate(timer);
            Vector3 dir = (transform.forward * _rollSpeed);
            _characterController.Move(dir * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        IsRooling = false;
    }

    IEnumerator Jumping()
    {
        IsJump = true;
        //pake delay, biar animasi jump jalan dulu sebelum character jump 
        yield return new WaitForSeconds(_jumpDelayDuration);
        
        _ySpeed = _jumpSpeed;
        while (_ySpeed > -0.05) {
            IsJump = true;

            yield return null;
        }
        IsJump = false;
    }
}

