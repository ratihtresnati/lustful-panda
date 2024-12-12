using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddQuest : MonoBehaviour
{
    public static AddQuest instance;
    public List<QuestSystem> initialQuests = new List<QuestSystem>();
    public List<QuestScriptable> quest = new List<QuestScriptable>(); // List untuk menyimpan quest awal

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (QuestScriptable questData in quest)
        {
            QuestSystem newQuest = new QuestSystem();
            newQuest.objective = new QuestSystem.Objective();
            // Konversi data dari questData ke newQuest
            newQuest.objective.objectiveId = questData.idQuest;
            newQuest.questName = questData.nameQuest;
            newQuest.questDescription = questData.descriptionQuest;
            newQuest.objective.amount = questData.amount;
            newQuest.objective.type = questData.objectiveType;
            newQuest.completed = questData.isComplete;

            Debug.Log("" + questData.objectiveType);
            // ... dan seterusnya
            initialQuests.Add(newQuest);
        }
    }
    void Start()
    {
        foreach (QuestSystem quest in initialQuests)
        {
            QuestLog.AddQuest(quest);
        }
    }
    
    private void Update()
    {

        foreach (var itemQuestSystem in initialQuests)
        {
            foreach (var item in quest)
            {
                if(itemQuestSystem.objective.objectiveId == item.idQuest)
                {
                    item.isComplete = itemQuestSystem.completed;
                }
            }
        }

        if (SaveSystemJSON.Instance.saved == false)
        {
            QuestManage();
        }
    }

    public void OnMonsterKilled(int monsterId)
    {
        QuestLog.CheckQuestObjective(QuestSystem.Objective.Type.kill, monsterId);
    }

    public void OnItemCollected(int itemId)
    {
        QuestLog.CheckQuestObjective(QuestSystem.Objective.Type.collect, itemId);
    }

    public void SceneTrigger(int sceneId)
    {
        QuestLog.CheckQuestObjective(QuestSystem.Objective.Type.trigger, sceneId);
    }

    public void QuestManage()
    {
        foreach (var item in initialQuests)
        {
            item.completed = false;
        }
        
        QuestLog.UpdateList();
    }
}
