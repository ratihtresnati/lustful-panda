using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestLog 
{
    private static List<QuestSystem> questList;
    private static List<QuestSystem> completedQuest;

    public delegate void OnQuestChange(List<QuestSystem> activeQuests, List<QuestSystem> completedQuest);
    public static event OnQuestChange onQuestChange;

    public static void Initialize() {
        completedQuest = new List<QuestSystem>();
        questList = new List<QuestSystem>();
        // completedQuest = new List<QuestSystem>();
    }

    // public static bool HasQuest(QuestSystem quest)
    // {
    //     return questList.Contains(quest);
    // }

    public static void AddQuest(QuestSystem quest) {

        if (quest == null)
        {
            Debug.LogError("Cannot add null QuestSystem to QuestLog.");
            return;
        }

        if (questList == null)
        {
            Debug.LogError("QuestList is null. Please initialize it.");
            return;
        }

        questList.Add(quest);
        // HandleOwnedItems(quest);
        onQuestChange.Invoke(questList, completedQuest);
        
        // onQuestChange.Invoke(completedQuest, questList);
    }
    
    public static void CheckQuestObjective(QuestSystem.Objective.Type type, int id) 
    {
        for (int i = questList.Count - 1; i >= 0; i--) 
        {
            QuestSystem quest = questList[i];
            if (quest.objective.CheckObjectiveCompleted(type, id)) 
            {
                CompleteQuest(quest);
                quest.completed = true;
                // UIQuestLoginstance.MoveCompletedQuestButtonToTop(quest);
            }
        }

        onQuestChange.Invoke(questList, completedQuest);
        // onQuestChange.Invoke(completedQuest, questList);
    }
    public static void CompleteQuest(QuestSystem quest) {
        completedQuest.Add(quest);
        questList.Remove(quest);
        
        onQuestChange.Invoke(questList, completedQuest);
        
        // onQuestChange.Invoke(completedQuest, questList);
    }

    private static void HandleOwnedItems(QuestSystem quest) {
        if (quest.objective.type != QuestSystem.Objective.Type.collect)
            return;
        int amount = 0; 
        if (quest.objective.ForceAddObjective(amount))
            CompleteQuest(quest);
    }

    private static void StageChanged(QuestSystem quest) {
        if (quest.objective.type != QuestSystem.Objective.Type.trigger)
            return;

        quest.objective.UpdateStageNum(1);
        int amount = 0; 
        if (quest.objective.CheckTriggerObjectiveCompleted(amount))
            CompleteQuest(quest);
    }

    public static QuestSystem getQuestNo(int index) {
        if (index < questList.Count)
            return questList[index];
        else
            return completedQuest[index - questList.Count];
    }

    public static List<QuestSystem> GetActiveQuests() 
    {
        return new List<QuestSystem>(questList); 
    }
}
