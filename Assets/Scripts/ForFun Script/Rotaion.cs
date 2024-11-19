using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotaion : MonoBehaviour
{
    public Transform origin;
    public float rotation = 0f;

    private void Update()
    {
        Vector3 originalVector = origin.forward * 5;
        Vector3 offset = Quaternion.Euler(0, rotation, 0) * originalVector;
        transform.position = origin.position + offset;
    }
}
