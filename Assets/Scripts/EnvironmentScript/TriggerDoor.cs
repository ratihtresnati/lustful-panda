using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerDoor : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private String _tag;
    public bool IsOpen { get; private set; }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            rigidbody.isKinematic = false;
            IsOpen = true;
        }
    }
}
