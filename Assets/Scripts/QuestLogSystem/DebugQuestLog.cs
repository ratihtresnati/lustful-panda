using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugQuestLog : MonoBehaviour
{
    
    private string[] questDescriptions = new string[]
    {
        "keluar dari kandang.",
        "ambil bambu.",
        "menuju cafetaria.",
        "lanjutkan quest berikutnya.",
        // "Selamatkan hewan peliharaan yang terjebak di dalam gua.",
        // "Hancurkan pasukan musuh yang menyerang desa.",
        // "Cari bunga ajaib di puncak bukit tertinggi.",
        // "Lindungi kereta barang dari serangan bandit.",
        // "Temukan dan kalahkan bos tersembunyi di hutan gelap.",
        // "Kumpulkan ramuan untuk membuat elixir penyembuhan."
    };

    void Start()
    {
        // AddQuests(4); 
    }

    private QuestSystem getNext(int i)
    {
        QuestSystem q = new QuestSystem();
        q.questName = "Stage " + i;
        q.questDescription = questDescriptions[i % questDescriptions.Length];

        // q.expReward = Random.Range(100, 1000);
        // q.goldReward = Random.Range(5, 20);
        q.questCategory = 0;

        q.objective = new QuestSystem.Objective();
        q.objective.type = (QuestSystem.Objective.Type)Random.Range(0, 3); 
        // q.objective.amount = Random.Range(2, 10);

        return q;
    }

    private void AddQuests(int iter)
    {
        for (int i = 1; i < iter; i++)
        {
            QuestLog.AddQuest(getNext(i));
        }
    }
}
