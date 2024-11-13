using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{ 
     public static QuestManager instance;
    private int _currentQuest;

    [SerializeField] private List<Quest> activeQuests = new List<Quest>();
    private bool hasInteractedWithGuard = false;
    private bool isDoorOpen = false; // Menyimpan status pintu

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
        InitializeQuests();
        StartQuest();
    }

    private void Update()
    {
        // Hapus kondisi _questIsComplete dari Update agar tidak otomatis mengganti quest.
    }

    private void InitializeQuests()
    {
        // activeQuests.Add(new Quest("Keluar dari kandang", "Pergilah keluar dari kandang untuk menuju kafetaria.", false));
        // activeQuests.Add(new Quest("Masuk ke kafetaria", "Temukan jalan ke kafetaria dan masuk ke dalamnya.", false));
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
            Debug.Log("Quest selesai: " + activeQuests[_currentQuest].questName);
            NextQuest();
        }
    }

    public void NextQuest()
    {
        _currentQuest++;
        if (_currentQuest < activeQuests.Count)
        {
            ShowHint(activeQuests[_currentQuest].questDescription);
            Debug.Log("Melanjutkan ke quest: " + activeQuests[_currentQuest].questName);
        }
    }

    public void ShowHint(string hint)
    {
        HintManager.instance.ShowHint(hint);
    }

    public void InteractWithDoorGuard()
    {
        if (!hasInteractedWithGuard && _currentQuest == 0)
        {
            ShowHint("Quest selesai! Anda telah keluar dari kandang.");
            hasInteractedWithGuard = true;
            CompleteCurrentQuest(); 
        }
        else if (_currentQuest == 1 && isDoorOpen) 
        {
            ShowHint("Sekarang, temukan jalan menuju kafetaria.");
            CompleteCurrentQuest();
        }
    }

    public void OpenDoor()
    {
        isDoorOpen = true; 
        Debug.Log("Pintu telah dibuka!");
        
       
        if (_currentQuest == 1)
        {
            ShowHint("Sekarang, temukan jalan menuju kafetaria.");
        }
    }
}
