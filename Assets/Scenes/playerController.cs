using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using UnityEditor.ShaderGraph.Drawing;

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


    [SerializeField] bool scriptLama;
    [SerializeField] bool keyframe;
    [SerializeField] private CharacterAnimatorControllerStateLama _characterAnimator;

    public GameObject panda;
    public GameObject box;

    public bool InBox;
    public bool PickHT;
    public bool GameOver;
    public bool PlayerSee;
    public bool IsCatch = false;

    [SerializeField]
    private Vector2 _inputVector;
    private Vector3 _velocity;
    public Vector3 Move { get; private set; }
    public bool IsJump { get; private set; }
    public bool IsRun { get; private set; }
    public bool IsRooling { get; private set; }
    public bool isGrounded { get; private set; }
    public float GetInputX() => _inputVector.x;
    public float GetInputY() => _inputVector.y;

    public Image StaminaBar;
    public float Stamina, MaxStamina;
    public float RunCost;
    public float ChargeRate;
    public float magnitude;

    Rigidbody rb;

    private Coroutine recharge;

    public ParticleSystem SmokeVFX;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        // GameOver = FindObjectOfType<GameOver>();
        panda = GameObject.Find("Panda");

        rb = GetComponent<Rigidbody>();

        Keyframe roll_lastFrame = _rollCurve[_rollCurve.length - 1];
        _rollTimer = roll_lastFrame.time;

        SmokeVFX.playbackSpeed = 1.5f;
        SmokeVFX.Stop();
    }

    void Update()
    {
        // Debug.Log(_ySpeed);
        StartCoroutine(HanddleGameOver());
        //HanddleGameOver();

        if (!IsCatch)
        {

            HanddleMovements();
            Action();

            TransformBox();

            if (InputManager.instance.RollInput)
            {
                if (!IsRooling)
                {
                    if (_characterController.isGrounded)
                    {
                        if (_velocity.magnitude != 0) StartCoroutine(Rolling());
                    }
                }
            }

            if (!InBox)
            {

                //moveset
                if (InputManager.instance.JumpInput)
                {
                    if (!IsRooling)
                    {
                        if (_characterController.isGrounded && !IsJump)
                        {
                            StartCoroutine(Jumping());
                        }
                    }
                }


                if (InputManager.instance.RunPressed)
                {
                    IsRun = true;
                    if (recharge != null)
                    {
                        StopCoroutine(recharge);
                    }
                }

                if (InputManager.instance.RunReleased)
                {
                    IsRun = false;
                    recharge = StartCoroutine(RechargeStamina());
                }

            }
        }
        //Debug.Log(ySpeed);
        // Debug.Log(_IsJump);
    }

    IEnumerator HanddleGameOver()
    {
        if (GameOver)
        {
            transform.gameObject.layer = 9;
            yield return new WaitForSeconds(0.1f);
            rb.isKinematic = true;
            _velocity.y = 0;
            IsCatch = true;
            if (scriptLama)
            {
                StartCoroutine(_characterAnimator.Catching());
            }
        }
    }

    /*
    private void HanddleGameOver()
    {
        if (GameOver)
        {

            rb.isKinematic = true;
            _velocity.y = 0;
            IsCatch = true;
        }
    }
    */

    private void TransformBox()
    {

        // Player ketahuan ketika terlihat Zoo Keeper
        if (InBox)
        {
            if (PlayerSee || IsRooling)
            {
                //SmokeVFX.Play();
                StartCoroutine(BecomeBox());
            }
        }


        // Ketika sedang dalam kondisi menjadi box
        if (InBox)
        {
            panda.SetActive(false); // objek panda hilang
            box.SetActive(true); // diganti object kardus
            transform.gameObject.layer = 0;


            if (magnitude > 0)
            {
                transform.gameObject.layer = 10; // layer mask berubah menjadi terget
            }
            else
            {
                transform.gameObject.layer = 0; // layer mask berubah menjadi default
            }

        }

        else if (PickHT)
        {
            transform.gameObject.layer = 16;
        }


    }

    private Vector3 lastForward;
public bool IsTurnLeft { get; private set; }
public bool IsTurnRight { get; private set; }


    IEnumerator BecomeBox()
    {
        yield return new WaitForSeconds(0.5f);
        InBox = false;
        panda.SetActive(true);
        box.SetActive(false);
        transform.gameObject.layer = 10;
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
                if (!IsJump)
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
            }
            else
            {
                IsRun = false;
                _speed = _walkSpeed;
            }

            magnitude = Mathf.Clamp01(Move.magnitude) * _speed;
            Move.Normalize();

            _ySpeed += Physics.gravity.y * Time.deltaTime;

            _velocity = Move * magnitude;

            _velocity.y = _ySpeed;

            _characterController.Move(_velocity * Time.deltaTime);

            if (keyframe)
            {
                if (Move != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(Move, Vector3.up);
                    float rotationStep = _rotationSpeed * Time.deltaTime;

                    // simpan arah lama
                    Vector3 currentForward = transform.forward;

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationStep);

                    // deteksi arah belok
                    Vector3 newForward = transform.forward;
                    Vector3 cross = Vector3.Cross(currentForward, newForward);

                    if (cross.y > 0.01f) // belok kiri
                    {
                        IsTurnLeft = false;
                        IsTurnRight = true;
                    }
                    else if (cross.y < -0.01f) // belok kanan
                    {
                        IsTurnLeft = true;
                        IsTurnRight = false;
                    }
                    else
                    {
                        IsTurnLeft = false;
                        IsTurnRight = false;
                    }

                    lastForward = newForward;
                }
            }
            else
            {
                if (Move != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(Move, Vector3.up);

                    float rotationStep = _rotationSpeed * Time.deltaTime;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationStep);
                }
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
        if (InBox)
        {
            SmokeVFX.Play();
        }

        gameObject.tag = "PandaRolling";
        float timer = 0;
        while (timer < _rollTimer)
        {
            IsRooling = true;
            float _rollSpeed = _rollCurve.Evaluate(timer);
            Vector3 dir = (transform.forward * _rollSpeed);
            _characterController.Move(dir * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        IsRooling = true;
        yield return new WaitForSeconds(0.1f);
        IsRooling = false;
    }

    IEnumerator Jumping()
    {
        IsJump = true;
        //pake delay, biar animasi jump jalan dulu sebelum character jump 
        yield return new WaitForSeconds(_jumpDelayDuration);

        _ySpeed = _jumpSpeed;
        while (_ySpeed > 0)
        {
            IsJump = true;

            yield return null;
        }
        IsJump = false;
    }

    public bool IsRest { get; private set; }
    public bool IsSit { get; private set; }
    public void Action()
    {
        if (Move == Vector3.zero && _characterController.isGrounded && !IsJump && !IsRooling)
        {
            InputManager.instance.IsAction = true;
        }
        else
        {
            InputManager.instance.IsAction = false;
        }
    }
}

