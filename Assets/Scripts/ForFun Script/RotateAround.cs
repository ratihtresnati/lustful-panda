using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float degrees;
    public Transform target;

    private void Update()
    {
        transform.RotateAround(target.position, transform.up, degrees * Time.deltaTime);
    }
}
