using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName;
    public string questDescription;
    public string targetItem;
    public bool isCompleted;

    public Quest(string name, string description, string item)
    {
        questName = name;
        questDescription = description;
        targetItem = item;
        isCompleted = false;
    }

    public void CompleteQuest()
    {
        isCompleted = true;
        Debug.Log(questName + " completed!");
    }
}
