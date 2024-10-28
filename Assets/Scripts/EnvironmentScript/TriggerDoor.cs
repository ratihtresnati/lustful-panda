using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerDoor : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Outline outline;
    [SerializeField] private String _tag;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            rigidbody.isKinematic = false;
            QuestManager.instance._questIsComplete = true;

            if(outline != null)
            {
                outline.ApplyOutline(false);
            }
        }
    }
}
