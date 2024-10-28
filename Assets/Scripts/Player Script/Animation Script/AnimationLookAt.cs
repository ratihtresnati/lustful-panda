using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationLookAt : MonoBehaviour
{
    public float duration = 0.3f;
    public Rig rightTurn;
    public Rig leftTurn;
    private Turn _currentState = Turn.Idle;

    private void Update()
    {
        float yRotation = transform.eulerAngles.y;

        if(yRotation == 270||yRotation == 90||yRotation == 180|| yRotation == 0)
        {
            _currentState = Turn.StopTurn;
        }

        if(yRotation == 45 || yRotation == 135 || yRotation == 315 || yRotation == 225)
        {
            _currentState = Turn.StopTurn;
        }  

        switch (_currentState)
        {
            case Turn.Idle:
                if(yRotation >= 25f && yRotation <= 65f || yRotation <= 330f && yRotation >= 300f)
                {
                    if(Input.GetKey("d"))
                    {
                        _currentState = Turn.Right;
                    }

                    if(Input.GetKey("a"))
                    {
                        _currentState = Turn.Left;
                    }
                }

                if( yRotation >= 30f && yRotation <= 60f || yRotation <= 150f && yRotation >= 120f)
                {
                    if(Input.GetKey("w"))
                    {
                        _currentState = Turn.Left;
                    }

                    if(Input.GetKey("s"))
                    {
                        _currentState = Turn.Right;
                    }
                }

                if(yRotation <= 150f && yRotation >= 120f || yRotation >= 210f && yRotation <= 240f)
                {
                    if(Input.GetKey("d"))
                    {
                        _currentState = Turn.Left;
                    }

                    if(Input.GetKey("a"))
                    {
                        _currentState = Turn.Right;
                    }
                }

                if(yRotation <= 330f && yRotation >= 300f || yRotation >= 210f && yRotation <= 240f)
                {
                    if(Input.GetKey("w"))
                    {
                        _currentState = Turn.Right;
                    }

                    if(Input.GetKey("s"))
                    {
                        _currentState = Turn.Left;
                    }
                }
                break;
            case Turn.Right:
                rightTurn.weight += Time.deltaTime * duration;
                break;
            case Turn.Left:
                leftTurn.weight += Time.deltaTime * duration;     
                break;
            case Turn.StopTurn:
                ResetWeight();
                break;
        }
    }

    private void ResetWeight()
    {
        rightTurn.weight -= Time.deltaTime * duration;

        if(rightTurn.weight == 0)
        {
            _currentState = Turn.Idle;
        }

        leftTurn.weight -= Time.deltaTime * duration;
                    
        if(leftTurn.weight == 0)
        {
            _currentState = Turn.Idle;
        }
    }
}
    
public enum Turn
    {
        Idle,
        Right,
        Left,
        StopTurn
    }