using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddQuest : MonoBehaviour
{
    public List<QuestSystem> initialQuests = new List<QuestSystem>(); // List untuk menyimpan quest awal

    void Start()
    {
        foreach (QuestSystem quest in initialQuests)
        {
            QuestLog.AddQuest(quest);
        }
    }
}
