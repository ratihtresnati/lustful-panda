using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PandaQuest : MonoBehaviour
{
    public bool isFirstQuest = true; 
    [SerializeField] private String _tag;
     void Update()
    {
        if (Input.GetKey(KeyCode.Q) && isFirstQuest)
        {
            QuestManager.instance.InteractWithDoorGuard(); 
        }
    } 
}   

