using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public string questName;
    [TextArea] public string questDescription;
    public int goldReward;
    public int expReward;
    public QuestSystem.Objective objective;

    [System.Serializable]
    public class Objective
    {
        public enum Type { Kill, Talk, Collect }
        public Type type;
        public int objectiveId; 
        public int amount; 
    }

    // public Objective objective;
}
