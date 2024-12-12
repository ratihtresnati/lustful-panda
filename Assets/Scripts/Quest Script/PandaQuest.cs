using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PandaQuest : MonoBehaviour
{
    // public float interactionRange = 2.0f;  
    // public Transform interactableObject;  
    public bool isFirstQuest = true; 
    [SerializeField] private String _tag;

    void Update()
    {
        if(Input.GetKey(KeyCode.Q) && isFirstQuest == true)
        {
            QuestManager.instance._questIsComplete = true;
            isFirstQuest = false;
        }
    }
}

