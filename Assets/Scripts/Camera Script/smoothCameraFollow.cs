using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothCameraFollow : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    [SerializeField]


    private Vector3 _offset;

    private float smoothTime = 0f;

    private Vector3 _currentVelocity = Vector3.zero;

    [SerializeField] 
    private Vector3 minValue, maxValue;

    private void Awake()
    {
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        
        transform.position = new Vector3(Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));

    }
}
