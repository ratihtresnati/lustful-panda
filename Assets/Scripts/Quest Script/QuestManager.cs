using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private int _currentQuest;
    private int _currentQuestIndex;
    private bool _questStart = false;
    // private bool _lastQuest;
    [SerializeField] private List<QuestScriptable> activeQuests = new List<QuestScriptable>();
    private void Update()
    {
        // Quest();
        Debug.Log(_currentQuestIndex + "" +activeQuests[_currentQuest].descriptionQuest.Count);


        if(_questStart == false)
        {
            Quest();
        }

        // activeQuests[_currentQuest].isComplete = false;
        activeQuests[_currentQuest].nextHint = false;
    }
    
    private void Quest()
    {
        //memanggil startquest dari quest dari sini sekalian melakukan start quest
        QuestUI.instance.StartQuest(this);
    }

    public void StartQuest()
    {
        ShowCurrentDescription();
    }

    public void ShowCurrentDescription()
    {
        if(_currentQuestIndex < activeQuests[_currentQuest].descriptionQuest.Count)
        {
            QuestUI.instance.ShowHint(activeQuests[_currentQuest].descriptionQuest[_currentQuestIndex]);
        }
    }

    public void NextDescription()
    {
        _currentQuestIndex++;

        if(_currentQuestIndex < activeQuests[_currentQuest].descriptionQuest.Count)
        {
            ShowCurrentDescription();
        }
        else
        {
            activeQuests[_currentQuest].isComplete = true;
            if (activeQuests[_currentQuest].isComplete)
            {
                _currentQuestIndex = 0;
                NextQuest(out bool lastQuest);
            }
        }
    }

    public void NextQuest(out bool lastQuest)
    {
        _currentQuest++;

        if (_currentQuest > activeQuests.Count - 1)
        {
            lastQuest = true;
            return;
        }

        lastQuest = false;

        ShowCurrentDescription();
    }

    public bool HintComplete()
    {
        return activeQuests[_currentQuest].nextHint;
    }

    public int IdQuest()
    {
        return activeQuests[_currentQuest].idQuest;
    }

    public int CurrentIndex()
    {
        return _currentQuestIndex;
    }

    // public void InteractWithDoorGuard()
    // {
    //     if (!hasInteractedWithGuard)
    //     {
    //         ShowHint("Cari bambu untuk diberikan kepada penjaga pintu.");
    //         hasInteractedWithGuard = true;
    //     }
    //     else if (bambooCollected)
    //     {
    //         ShowHint("Quest selesai! Anda telah memberikan bambu.");
    //         _questIsComplete = true;
    //         NextQuest();
    //     }
    //     else
    //     {
    //         ShowHint("Ambil bambu untuk diberikan.");
    //     }
    // }

    // public void CollectBamboo()
    // {
    //     bambooCollected = true;
    //     if (hasInteractedWithGuard)
    //     {
    //         ShowHint("Bambu siap diberikan kepada penjaga pintu.");
    //     }
    //     else
    //     {
    //         ShowHint("Kembali dan berinteraksilah dengan penjaga pintu.");
    //     }
    // }
}


