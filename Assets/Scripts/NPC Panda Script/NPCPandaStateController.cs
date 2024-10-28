using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPandaStateController : MonoBehaviour
{
    [Header("Animasi")]
    [SerializeField] private float _acceleration = 1f; //naikin kalo mau animasi rest & sit lebih cepet
    [SerializeField] private NPCPanda _currentState = NPCPanda.Idle;
    [SerializeField] private Animator _animator; 
    private float _velocity = 0.0f;
    [SerializeField] private float _idleTime = 6f; //naikin kalo mau animasi idle lebih lama
    private float _timeBeforeIdle = 0; 
    [SerializeField] private bool _isIdleWithTime = false;
    [SerializeField] private bool _isRest = false;
    [SerializeField] private bool _isSit = false;

    [Header("Move")]
    [SerializeField] private Transform _targetMove;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _targetRotation;
    [SerializeField] private float _rotationSpeed;
    public float maxDistance = 1.0f;

    [Header("Quest")]
    [SerializeField] private bool _isQuest = false;
    public bool _isComplete = false;

    void Update()
    {
        switch (_currentState)
        {
            case NPCPanda.Idle:
                _timeBeforeIdle += Time.deltaTime;
                if(_timeBeforeIdle > _idleTime)
                {
                    if (_isRest == true && _isSit == true)
                    {
                        _timeBeforeIdle = 0;
                        RandomIdles();
                    }

                    if(_isRest == true)
                    {
                        _timeBeforeIdle = 0;
                        _currentState = NPCPanda.Rest;
                    }

                    if(_isSit == true && _isQuest == true || _isSit == true)
                    {
                        _timeBeforeIdle = 0;
                        _currentState = NPCPanda.Sit;
                    }
                }
                break;
            case NPCPanda.Sit:
                _animator.SetBool("isIdle", true);
                _animator.SetFloat("Idle", 1.1f);

                //tambahin kalo quest udh beres
                if (_isQuest == true  && _isComplete == true)
                {
                    VelocityCounting();
                    if (_velocity > _idleTime)
                    {   
                        VelocityReset();
                        _currentState = NPCPanda.UpSit;
                    }
                }
                else if (_isIdleWithTime == true)
                {
                    VelocityCounting();
                    if (_velocity > _idleTime)
                    {   
                        VelocityReset();
                        _currentState = NPCPanda.UpSit;
                    }
                }
                break;
            case NPCPanda.UpSit:
                _animator.SetFloat("Idle", 5.1f);
                
                VelocityCounting();
                if (_velocity > _idleTime)
                {   
                    VelocityReset();
                    
                    if(_isQuest == true)
                    {
                        _animator.SetBool("isIdle", false);
                        _animator.SetFloat("Idle", 0f);
                        _currentState = NPCPanda.Move;
                    }
                    else if(_isIdleWithTime == true)
                    {
                        _animator.SetBool("isIdle", false);
                        _animator.SetFloat("Idle", 0f);
                        _currentState = NPCPanda.Idle;
                    }
                }
                break;
                case NPCPanda.Rest:
                _animator.SetBool("isIdle", true);
                _animator.SetFloat("Idle", 0.1f);

                if(_isIdleWithTime == true)
                {
                    VelocityCounting();
                    if (_velocity > _idleTime)
                    {
                        VelocityReset();
                        _currentState = NPCPanda.UpRest;
                    }
                }
                break;
            case NPCPanda.UpRest:
                _animator.SetFloat("Idle", 5.1f);

                VelocityCounting();
                if (_velocity > _idleTime)
                {
                    VelocityReset();
                    _animator.SetBool("isIdle", false);
                    _animator.SetFloat("Idle", 0f);
                    _currentState = NPCPanda.Idle;
                }
                break;
                case NPCPanda.Move:
                _agent.destination = _targetMove.position;
                _animator.SetFloat("speed", _agent.velocity.magnitude);

                float distance = Vector3.Distance(_targetMove.position, transform.position);
                if(distance < maxDistance + 0.5f)
                {
                    //rotation
                    Quaternion tr = Quaternion.Euler(0, _targetRotation, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * _rotationSpeed);

                    if (Mathf.Abs(transform.rotation.eulerAngles.y - tr.eulerAngles.y) < 0.001f)
                    {
                        _isQuest = false;
                        _currentState = NPCPanda.Sit;
                    }
                }
                break;
        }
    }

    private void RandomIdles()
    {
        float rd = Random.Range(0f, 2f);

        if(rd < 1)
        {
            _currentState = NPCPanda.Rest;
        } 
        else 
        {
            _currentState = NPCPanda.Sit;
        }
    }

    private float VelocityReset()
    {
        return _velocity = 0.0f;
    }

    private void VelocityCounting()
    {
        _velocity += Time.deltaTime * _acceleration;
    }
}


public enum NPCPanda
{
    Idle,
    Sit,
    UpSit,
    Rest,
    UpRest,
    Move,
    
}