using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestScriptable : ScriptableObject
{
    public int idQuest;
    public string nameQuest;
    public string descriptionQuest;
    public QuestSystem.Objective.Type objectiveType;
    public bool isComplete = false;
    public int amount;
}
