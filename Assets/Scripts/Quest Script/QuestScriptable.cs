using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestScriptable : ScriptableObject
{
    public int idQuest;
    // public List<string> descriptionQuest;
    public string nameQuest;
    public string descriptionQuest;
    // public string objective;
    public QuestSystem.Objective.Type objectiveType;
    public bool isComplete = false;

    public int amount;
    // // [System.NonSerialized]
    // public int currentAmount;

    // public bool CheckObjectiveCompleted(int id) {
    //     if (id == idQuest)
    //         currentAmount++;
    //     return currentAmount >= amount;
    // }

    // public bool CheckTriggerObjectiveCompleted(int amount) {
    //         currentAmount += amount;
    //         return currentAmount >= amount;
    //     }
}
