using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public static QuestUI instance { get; private set; }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public TMP_Text hintText;
    private QuestManager currentQuestManager;
    private bool isComplete = false;

    private void Update()
    {
        if (currentQuestManager == null) return;
    }

    public void StartQuest(QuestManager questManager)
    {
        //initiate quest manager dari quest manager 
        currentQuestManager = questManager;

        currentQuestManager.StartQuest();
    }

    public void NextQuest()
    {
        currentQuestManager.NextDescription();
    }
    
    public void ShowHint(string hint)
    {
        hintText.text = hint;
    }
}
