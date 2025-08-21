using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationLookAtDerajat : MonoBehaviour
{
    public float duration = 0.3f;
    public Rig rightTurn;
    public Rig leftTurn;
    private TurnD _currentState = TurnD.Idle;

    private void Update()
    {
        float yRotation = transform.eulerAngles.y;

        if(yRotation == 270||yRotation == 90||yRotation == 180|| yRotation == 0)
        {
            _currentState = TurnD.StopTurn;
        }

        if(yRotation == 45 || yRotation == 135 || yRotation == 315 || yRotation == 225)
        {
            _currentState = TurnD.StopTurn;
        }  

        switch (_currentState)
        {
            case TurnD.Idle:

            if(Input.GetKey("m"))
            {
                _currentState = TurnD.Right;
            }else
            {
                _currentState = TurnD.StopTurn;
            }

        if(Input.GetKey("n"))
        {
            _currentState = TurnD.Left;
        }else
        {
            _currentState = TurnD.StopTurn;
        }

                if(yRotation >= 25f && yRotation <= 65f || yRotation <= 330f && yRotation >= 300f)
                {
                    if(Input.GetKey("d"))
                    {
                        _currentState = TurnD.Right;
                    }

                    if(Input.GetKey("a"))
                    {
                        _currentState = TurnD.Left;
                    }
                }

                if( yRotation >= 30f && yRotation <= 60f || yRotation <= 150f && yRotation >= 120f)
                {
                    if(Input.GetKey("w"))
                    {
                        _currentState = TurnD.Left;
                    }

                    if(Input.GetKey("s"))
                    {
                        _currentState = TurnD.Right;
                    }
                }

                if(yRotation <= 150f && yRotation >= 120f || yRotation >= 210f && yRotation <= 240f)
                {
                    if(Input.GetKey("d"))
                    {
                        _currentState = TurnD.Left;
                    }

                    if(Input.GetKey("a"))
                    {
                        _currentState = TurnD.Right;
                    }
                }

                if(yRotation <= 330f && yRotation >= 300f || yRotation >= 210f && yRotation <= 240f)
                {
                    if(Input.GetKey("w"))
                    {
                        _currentState = TurnD.Right;
                    }

                    if(Input.GetKey("s"))
                    {
                        _currentState = TurnD.Left;
                    }
                }
                break;
            case TurnD.Right:
                rightTurn.weight += Time.deltaTime * duration;
                break;
            case TurnD.Left:
                leftTurn.weight += Time.deltaTime * duration;     
                break;
            case TurnD.StopTurn:
                ResetWeight();
                break;
        }
    }

    private void ResetWeight()
    {
        rightTurn.weight -= Time.deltaTime * duration;

        if(rightTurn.weight == 0)
        {
            _currentState = TurnD.Idle;
        }

        leftTurn.weight -= Time.deltaTime * duration;
                    
        if(leftTurn.weight == 0)
        {
            _currentState = TurnD.Idle;
        }
    }
}
    
public enum TurnD
    {
        Idle,
        Right,
        Left,
        StopTurn
    }