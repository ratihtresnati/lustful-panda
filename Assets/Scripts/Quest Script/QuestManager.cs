using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;  
    private int _currentQuest;

    [SerializeField] private List<Quest> activeQuests = new List<Quest>();  
    [SerializeField] private bool _firstQuest = false;
    public bool _questIsComplete = false;
    public bool isNextQuest = false;
    public UnityEvent unityEvent;

    void Awake()
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

    private void Start()
    {
        StartQuest();
    }

    private void Update()
    {
        Debug.Log(_currentQuest);
        // Quest(_currentQuest);   

        if(_questIsComplete == true)
        {
            activeQuests[_currentQuest].CompleteQuest();
            activeQuests[_currentQuest].EndQuest();
        }
    }

    
    public void AddQuest(Quest newQuest)
    {
        activeQuests.Add(newQuest);
        Debug.Log("Quest: " + newQuest.questName);
        HintManager.instance.ShowHint(newQuest.questDescription);
    }

    
    public void ItemCollected(string itemName)
    {
        foreach (Quest quest in activeQuests)
        {
            if (quest.targetItem == itemName && !quest.isCompleted)
            {
                quest.CompleteQuest();
                Debug.Log("Quest Selesai: " + "Temukan Pintu Untuk Membukanya");
                HintManager.instance.ShowHint("Temukan pintu untuk membukanya.");

                NextQuest();
            }
        }
    }

    private void StartQuest()
    {
        ShowHint(activeQuests[_currentQuest].questDescription);
        _firstQuest = false;
    }

    public void ResetQuest()
    {
        isNextQuest = false;
        _questIsComplete = false;
    }

    public void NextQuest()
    {
        if (isNextQuest == false)
        {
            isNextQuest = true;
            _currentQuest++;
            if (_currentQuest < activeQuests.Count - 1)
            {
                StartQuest();
            }
        }
    }

    public void ShowHint(string hint)
    {
        HintManager.instance.ShowHint(hint);
    }
}
