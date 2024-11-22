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
        questList = new List<QuestSystem>();
        completedQuest = new List<QuestSystem>();
    }

    public static void AddQuest(QuestSystem quest) {
        questList.Add(quest);
        HandleOwnedItems(quest);
        onQuestChange.Invoke(questList, completedQuest);
    }

    public static void CheckQuestObjective(QuestSystem.Objective.Type type, int id) {
        foreach (QuestSystem quest in questList)
            if (quest.objective.CheckObjectiveCompleted(type, id))
                CompleteQuest(quest);
        onQuestChange.Invoke(questList, completedQuest);
    }

    public static void CompleteQuest(QuestSystem quest) {
        questList.Remove(quest);
        completedQuest.Add(quest);
        
        onQuestChange.Invoke(questList, completedQuest);
    }

    private static void HandleOwnedItems(QuestSystem quest) {
        if (quest.objective.type != QuestSystem.Objective.Type.collect)
            return;
        int amount = 0; 
        if (quest.objective.ForceAddObjective(amount))
            CompleteQuest(quest);
    }

    public static QuestSystem getQuestNo(int index) {
        if (index < questList.Count)
            return questList[index];
        else
            return completedQuest[index - questList.Count];
    }
}
