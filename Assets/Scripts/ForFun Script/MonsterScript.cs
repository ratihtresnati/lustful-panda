using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterScript : MonoBehaviour
{

    [SerializeField]
    Transform _target;
    [SerializeField]
    Transform _headBone;

    [SerializeField]
    float _turnSpeed;
    [SerializeField]
    float _moveSpeed;

    [SerializeField]
    float turnAcceleration;
    [SerializeField]
    float moveAcceleratiaon;

    [SerializeField]
    float minDistToTarget;
    [SerializeField]
    float maxDistToTarget;
    
    [SerializeField]
    float maxAngleToTarget;

    Vector3 currentVelocity;

    float currentAngularVelocity;

    [SerializeField]
    LegStepper _frontLeftLegStepper;
    [SerializeField]
    LegStepper _frontRightLegStepper;
    [SerializeField]
    LegStepper _BackLeftLegStepper;
    [SerializeField]
    LegStepper _BackRightLegStepper;

    [SerializeField]
    float _headMaxTurnAngle;
    [SerializeField]
    float _headTracingSpeed;


    private void Awake()
    {
        StartCoroutine(LegUpdatCoroutine());
    }
    private void LateUpdate()
    {
        RootMotionUpdate();
        HeadTracing();
    }

    void HeadTracing()
    {
        Quaternion currentLocalRotation = _headBone.localRotation;

        _headBone.localRotation = Quaternion.identity;

        Vector3 targetWorldLookDir = -(_target.position - _headBone.position);
        Vector3 targetLocalLookDir = _headBone.InverseTransformDirection(targetWorldLookDir);

        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * _headMaxTurnAngle,
            0
            );

        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

        _headBone.localRotation = Quaternion.Slerp(
            currentLocalRotation,
            targetLocalRotation,
            1 - Mathf.Exp(-_headTracingSpeed * Time.deltaTime)
            );

    }

    void RootMotionUpdate()
    {
        Vector3 towardTarget = _target.position - transform.position;

        Vector3 towardTargetProjected = Vector3.ProjectOnPlane(towardTarget, transform.up);

        float angToTarget = Vector3.SignedAngle(transform.forward, towardTargetProjected, transform.up);

        float targetAngularVelocity = 0;

        if (Mathf.Abs(angToTarget) > maxAngleToTarget)
        {

            if (angToTarget > 0) 
            {
                targetAngularVelocity = _turnSpeed;
            }

            else
            {
                targetAngularVelocity = -_turnSpeed;
            }

            currentAngularVelocity = Mathf.Lerp(
                currentAngularVelocity,
                targetAngularVelocity,
                1 - Mathf.Exp(-turnAcceleration * Time.deltaTime)
                );

        }

        transform.Rotate(0, Time.deltaTime * -currentAngularVelocity, 0, Space.World);


        Vector3 targetVelocity = Vector3.zero;

        if (Mathf.Abs(angToTarget) > 90)
        {
            float distToTarget = Vector3.Distance(transform.position, _target.position);

            if (distToTarget > maxDistToTarget)
            {
                targetVelocity = _moveSpeed * towardTargetProjected.normalized;
            }

            else if (distToTarget < minDistToTarget)
            {
                targetVelocity = _moveSpeed * -towardTargetProjected.normalized;
            }
        }

        currentVelocity = Vector3.Lerp(
            currentVelocity,
            targetVelocity,
            1 - Mathf.Exp(-moveAcceleratiaon * Time.deltaTime)
            );

        transform.position += currentVelocity * Time.deltaTime;
    }

    IEnumerator LegUpdatCoroutine()
    {

        while (true)
        {

            do
            {
                _frontLeftLegStepper.TryMove();
                _BackRightLegStepper.TryMove();
                yield return null;
            } while (_BackRightLegStepper.Moving || _frontLeftLegStepper.Moving);

            do
            {
                _frontRightLegStepper.TryMove();
                _BackLeftLegStepper.TryMove();
                yield return null;
            } while (_BackLeftLegStepper.Moving || _frontRightLegStepper.Moving);
        }
    }
}
