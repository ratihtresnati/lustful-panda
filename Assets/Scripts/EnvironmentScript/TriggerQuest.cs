using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuest : MonoBehaviour
{
    public int questId; 
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("PandaMC") && !hasTriggered) 
        {
            AddQuest.instance.SceneTrigger(questId);
            Debug.Log("hii");
            hasTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        hasTriggered = false;
    }
}
