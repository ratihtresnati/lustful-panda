using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestScriptable : ScriptableObject
{
    public int idQuest;
    public List<string> descriptionQuest;
    public bool nextHint = false;
    public bool isComplete = false;
}
