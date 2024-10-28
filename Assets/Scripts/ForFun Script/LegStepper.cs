using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LegStepper : MonoBehaviour
{
    [SerializeField]
    private Transform _homeTransform;    
    [SerializeField]
    private float _wantStepAtDistance;    
    [SerializeField]
    private float _moveDuration;
    [SerializeField]
    float _stopOvershootFraction;

    public bool Moving;

    IEnumerator MoveToHome()
    {
        Moving = true;

        Quaternion startRot = transform.rotation;
        Vector3 startPoint = transform.position;

        Quaternion endRot = _homeTransform.rotation;
        Vector3 endPoint = _homeTransform.position;

        float timeElapsed = 0;

        do
        {
            timeElapsed += Time.deltaTime;

            float normalizedTime = timeElapsed / _moveDuration;

            transform.position = Vector3.Lerp(startPoint, endPoint,normalizedTime);
            transform.rotation = Quaternion.Lerp(startRot, endRot,normalizedTime);

            yield return null;
        }
        while (timeElapsed < _moveDuration);

        Moving = false;

    }

    IEnumerator Move() 
    {
        Moving = true;

        Vector3 startPoint = transform.position;
        Quaternion startRot = transform.rotation;

        Quaternion endRot = _homeTransform.rotation;

        Vector3 towardHome = (_homeTransform.position - transform.position);

        float overshootDistance = _wantStepAtDistance * _wantStepAtDistance;
        Vector3 overshootVector = towardHome * overshootDistance;

        overshootVector = Vector3.ProjectOnPlane(overshootVector, Vector3.up);

        Vector3 endPoint = _homeTransform.position + overshootVector;

        Vector3 centerPoint = (startPoint +endPoint);

        centerPoint += _homeTransform.up * Vector3.Distance(startPoint, endPoint) / 2f;

        float timeElapsed = 0;
        do
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / _moveDuration;

            transform.position =
                Vector3.Lerp(
                    Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                    Vector3.Lerp(centerPoint, endPoint, normalizedTime),
                    normalizedTime);

            transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);
            yield return null;
        }
        while (timeElapsed < _moveDuration);

        Moving = false;
    }

    public void TryMove()
    {
        if (Moving) return;

        float distFromHome = Vector3.Distance(transform.position, _homeTransform.position);

        if (distFromHome > _wantStepAtDistance)
        {
            StartCoroutine(Move());
        }
    }
}
