using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public string questName;
    public string questDescription;
    public string targetItem;
    public bool isCompleted;
    public UnityEvent unityEvent;

    public Quest(string name, string description, bool completed, string item = "")
    {
        questName = name;
        questDescription = description;
        targetItem = item;
        isCompleted = completed;
    }

    public void CompleteQuest()
    {
        isCompleted = true;
        Debug.Log("Quest completed: " + questName);
    }

    public void EndQuest()
    {
        unityEvent?.Invoke();
        Debug.Log("Ending quest: " + questName);
    }
}
