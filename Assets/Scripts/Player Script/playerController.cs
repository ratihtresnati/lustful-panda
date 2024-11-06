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
    private GameInput _gameInput;
    private Vector2 _inputVector;
    private Vector3 _velocity;
    public Vector3 move { get; private set; } 
    public bool IsJump { get; private set; }
    public bool IsRun { get; private set; }
    public bool IsRooling { get; private set; }
    public bool isGrounded { get; private set; }
    public bool isRotating { get; private set; }

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _gameInput = GetComponentInChildren<GameInput>();

        Keyframe roll_lastFrame = _rollCurve[_rollCurve.length - 1];
        _rollTimer = roll_lastFrame.time;

        _gameInput.OnRunningEvent += OnRunEvent;
        _gameInput.OutRunningEvent += OutRunEvent;
        _gameInput.OnJumpingEvent += OnJumpEvent;
        _gameInput.OnRollingEvent += OnRollEvent;
    }
    // Update is called once per frame
    void Update()
    {
        HanddleMovements();
        // //Debug.Log(_ySpeed);
        // Debug.Log(IsJump);
    }

      private void HanddleMovements()
    {
        if (!IsRooling)
        {
        gameObject.tag = "PandaMC";

        _inputVector = _gameInput.GetMovementControl();
        move = new Vector3(_inputVector.x, 0, _inputVector.y);

        if (IsRun)
        {
            _speed = _runSpeed;
        }
        else
        {
            IsRun = false;
            _speed = _walkSpeed;
        }

        float magnitude = Mathf.Clamp01(move.magnitude) * _speed;
        move.Normalize();

        _ySpeed += Physics.gravity.y * Time.deltaTime;

        _velocity = move * magnitude;

        _velocity.y = _ySpeed;

        _characterController.Move(_velocity * Time.deltaTime);

        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            // float angleDifference = Quaternion.Angle(transform.rotation, toRotation);
            
            // if (angleDifference > 0.1f)
            // {
                float rotationStep = _rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationStep);
            //     isRotating = true; 
            // }
            // else
            // {
            //     isRotating = false;
            // }

            // Debug.Log(isRotating);
        }

        if (_characterController.isGrounded)
        {
            isGrounded = true;
            _ySpeed = 0;
        }
        }
    }

    // Running Event

    // On Running
    private void OnRunEvent(object sander, EventArgs e)
    {

        IsRun = true;

    }

    // Out Running
    private void OutRunEvent(object sender, EventArgs e)
    {
        IsRun = false;
    }

    private void OnRollEvent(object sender, EventArgs e)
    {
        if (!IsRooling)
        { 
            if (!IsJump)
            {
                if (_velocity.magnitude != 0) StartCoroutine(Rolling());
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

    private void OnJumpEvent(object sander, EventArgs e)
    {
        if (!IsRooling)
        {
            if (_characterController.isGrounded && !IsJump)
            {
                StartCoroutine(Jumping());
            }
        }
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

