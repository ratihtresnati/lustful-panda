using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private int _currentQuest;

    [SerializeField] private List<Quest> activeQuests = new List<Quest>(); 
    public bool _questIsComplete = false;
    private bool bambooCollected = false;
    private bool hasInteractedWithGuard = false;
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
        if (_questIsComplete == true)
        {
            CompleteCurrentQuest(); 
        }
    }

    private void StartQuest()
    {
        if (_currentQuest < activeQuests.Count)
        {
            ShowHint(activeQuests[_currentQuest].questDescription);
            Debug.Log("Memulai quest: " + activeQuests[_currentQuest].questName);
        }
    }

    public void CompleteCurrentQuest()
    {
        
        if (_currentQuest < activeQuests.Count)
        {
            activeQuests[_currentQuest].CompleteQuest();
            activeQuests[_currentQuest].EndQuest();
            _questIsComplete = false;
            // NextQuest(); 
        }
    }

    public void NextQuest()
    {
        _currentQuest++;
        if (_currentQuest < activeQuests.Count)
        {
            Debug.Log("Melanjutkan ke quest: " + activeQuests[_currentQuest].questName);
            ShowHint(activeQuests[_currentQuest].questDescription);
        }
    }

    public void ShowHint(string hint)
    {
        HintManager.instance.ShowHint(hint);
        Debug.Log("Hint ditampilkan: " + hint);
    }

    public void InteractWithDoorGuard()
    {
        if (!hasInteractedWithGuard)
        {
            ShowHint("Cari bambu untuk diberikan kepada penjaga pintu.");
            hasInteractedWithGuard = true;
        }
        else if (bambooCollected)
        {
            ShowHint("Quest selesai! Anda telah memberikan bambu.");
            _questIsComplete = true;
            NextQuest();
        }
        else
        {
            ShowHint("Ambil bambu untuk diberikan.");
        }
    }

    public void CollectBamboo()
    {
        bambooCollected = true;
        if (hasInteractedWithGuard)
        {
            ShowHint("Bambu siap diberikan kepada penjaga pintu.");
        }
        else
        {
            ShowHint("Kembali dan berinteraksilah dengan penjaga pintu.");
        }
    }
}
