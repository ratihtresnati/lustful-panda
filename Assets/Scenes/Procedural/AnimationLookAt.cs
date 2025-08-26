using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using System;

public class AnimationLookAt : MonoBehaviour
{
    [SerializeField] private MultiAimConstraint aimConstraint;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float _resetDelay = 0.2f;

    private WeightedTransformArray sources;
    private static Vector2 _previousDirection;
    private Vector2 _currentDirection;
    private float _angleDifference;
    private bool _right = false;
    private bool _left = false;
    private float _resetTimer = 0f;

    private void Awake()
    {
        sources = aimConstraint.data.sourceObjects;
        _previousDirection = Vector2.zero;
    }

    private void Update()
    {
        Turn turn = TurnDirection();

        switch (turn)
        {
            case Turn.Left:
                TurnLeft();
                break;
            case Turn.Right:
                TurnRight();
                break;
            case Turn.Idle:
                IdleState();
                break;
        }

        aimConstraint.data.sourceObjects = sources;
    }

    private void IdleState()
    {
        if (_right)
        {
            _resetTimer += Time.deltaTime;
            if (_resetTimer < _resetDelay) return;
            _right = false;
        }
        if (_left)
        {
            _resetTimer += Time.deltaTime;
            if (_resetTimer < _resetDelay) return;
            _left = false;
        }

        // turunkan semua weight ke 0
        for (int i = 0; i < sources.Count; i++)
        {
            float current = sources.GetWeight(i);
            sources.SetWeight(i, Mathf.MoveTowards(current, 0f, duration * Time.deltaTime));
        }
    }

    private void TurnLeft()
    {
        sources.SetWeight(0, Mathf.MoveTowards(sources.GetWeight(0), 1f, duration * Time.deltaTime));
        sources.SetWeight(1, 0f); // kanan = 0
        _resetTimer = 0f;

        _right = false;
        _left = true;
    }

    private void TurnRight()
    {
        sources.SetWeight(1, Mathf.MoveTowards(sources.GetWeight(1), 1f, duration * Time.deltaTime));
        sources.SetWeight(0, 0f); // kiri = 0
        _resetTimer = 0f;

        _right = true;
        _left = false;
    }

    public Turn TurnDirection()
    {
        Vector2 rawInput = InputManager.instance.GetMovementControl();
        if (rawInput.magnitude < 0.1f) rawInput = Vector2.zero;
        _currentDirection = rawInput;
        
        // world direction berdasarkan input
        float angleInRadians = transform.eulerAngles.y * Mathf.Deg2Rad;
        Vector2 worldDirection = new Vector2(
            _currentDirection.x * Mathf.Cos(angleInRadians) - _currentDirection.y * Mathf.Sin(angleInRadians),
            _currentDirection.x * Mathf.Sin(angleInRadians) + _currentDirection.y * Mathf.Cos(angleInRadians));

        _angleDifference = Vector2.SignedAngle(_previousDirection, worldDirection);

        Turn turnDirection = Turn.Idle;

        if (_angleDifference > 0.1f)
        {
            if (_left == false) turnDirection = Turn.Right;
        }
        else if (_angleDifference < -0.1f)
        {
            if (_right == false) turnDirection = Turn.Left;
        }

        _previousDirection = worldDirection;

        return turnDirection;
    }
}

public enum Turn
{
    Idle,
    Right,
    Left
}
