using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private int _currentQuest;

    [SerializeField] private List<Quest> activeQuests = new List<Quest>(); 
    public bool _questIsComplete = false;
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
        
        activeQuests.Add(new Quest("Quest Pertama", "Berguling untuk menghancurkan pintu", "Pintu", false));
        
        
        activeQuests.Add(new Quest("Quest Kedua", "Hancurkan pintu dengan berguling", "Pintu", false));

        
        activeQuests.Add(new Quest("Quest Ketiga", "Cari jalan keluar", "", false));

        StartQuest();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentQuest == 0)
            {
                OnRollAction(); 
            }
            else if (_currentQuest == 1 && IsNearDoor())
            {
                AttemptToDestroyDoor(); 
            }
            else if (_currentQuest == 2)
            {
                ExitAreaReached(); 
            }
        }

        if (_questIsComplete)
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
            NextQuest(); 
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

    public void ItemCollected(string itemName)
    {
        if (activeQuests[_currentQuest].targetItem == itemName && !activeQuests[_currentQuest].isCompleted)
        {
            activeQuests[_currentQuest].CompleteQuest();
            activeQuests[_currentQuest].EndQuest();
            HintManager.instance.ShowHint("");
            NextQuest();
        }
    }

    public void OnRollAction() 
    {
        if (_currentQuest == 0)
        {
            Debug.Log("Quest pertama selesai, melanjutkan ke quest kedua.");
            _questIsComplete = true; 
        }
    }

    private void AttemptToDestroyDoor()
    {
        
        if (IsNearDoor() && CanRoll()) 
        {
            DestroyDoor(); 
        }
    }

    public void DestroyDoor()
    {
        if (_currentQuest == 1) 
        {
            Debug.Log("Pintu telah dihancurkan!");
            _questIsComplete = true; 
        }
    }

    private bool IsNearDoor()
    {
        return true; 
    }
    
    private bool CanRoll()
    {
        
        return true; 
    }

    public void ExitAreaReached()
    {
        if (_currentQuest == 2) 
        {
            Debug.Log("Quest selesai: Cari jalan keluar!");
            _questIsComplete = true; 
        }
    }
}
