using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Outline outline;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PandaRolling"))
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
