using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.ShaderGraph.Drawing;

// using System.Xml.Serialization;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    public Image StaminaBar;
    public float Stamina, MaxStamina;
    public float RunCost;
    public float ChargeRate;

    private Coroutine recharge;

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
            if (recharge != null)
            {
                StopCoroutine(recharge);
            }
        }

        if(InputManager.instance.RunReleased){
            IsRun = false;
            recharge = StartCoroutine(RechargeStamina());
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

        if (IsRun && Stamina > 0)
        {
            _speed = _runSpeed;
                Stamina -= RunCost * Time.deltaTime;
                if (Stamina < 0)
                {
                    Stamina = 0;
                }
                else
                {
                    StaminaBar.fillAmount = Stamina / MaxStamina;
                }
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

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (Stamina < MaxStamina)
        {
            Stamina += ChargeRate * Time.deltaTime;
            if (Stamina > MaxStamina)
            {
                Stamina = MaxStamina;
            }
            else
            {
                StaminaBar.fillAmount = Stamina / MaxStamina;
            }
            yield return new WaitForSeconds(.1f);
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

