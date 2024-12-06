// #define OLD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class QuestManager : MonoBehaviour
{ 
    public static QuestManager instance;
    public ButtonQuest[] quest;
    [SerializeField] private GameObject _questCanvas;
    private bool _isPause = false;
    
    GameObject selectedButton;
    private SelectButtonHandler selectButtonHandler;
    private ButtonSelected buttonSelected;

    public int currentStage = 0; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        selectButtonHandler = gameObject.GetComponent<SelectButtonHandler>();
        buttonSelected = gameObject.GetComponent<ButtonSelected>(); 
    }

    private void Update()
    {
        ButtonFromQuestLog();

        selectButtonHandler.SelectButton();

        if(selectButtonHandler.SelectedButton != null)
        {
            selectedButton = selectButtonHandler.SelectedButton;
        }

        foreach (var buttonSetting in quest)
        {
            if (selectedButton == buttonSetting.gameObject)
            {
                int index = System.Array.IndexOf(quest, buttonSetting);
                SelectButton(index);
                break;
            }
        }

        // if()
    }

    public void SelectButton(int index)
    {
        selectedButton = quest[index].gameObject;
        EventSystem.current.SetSelectedGameObject(quest[index].gameObject);

        for (int i = 0; i <= quest.Length; i++)
        {
            if (i == index)
            {
                // Debug.Log(i + " " + currentStage);
                UIQuestLog.instance.QuestPress(quest[index].button);
            }
        }

        // var selectableQuests = GetSelectableQuests().ToList();

        // selectedButton = quest[index].gameObject;
        // EventSystem.current.SetSelectedGameObject(quest[index].gameObject);

        // foreach (var item in selectableQuests)
        // {
        //     Debug.Log(item);
        // }


        // Debug.Log(selectableQuests.Count);

        // for (int i = 0; i <= quest.Length; i++)
        // {
        //     if (i == index)
        //     {
        //         if (index < selectableQuests.Count)
        //         {
        //             UIQuestLog.instance.QuestPress(quest[index].button);
        //         }
        //     }
        // }
    }

    // private IEnumerable<ButtonQuest> GetSelectableQuests()
    // {
    //     return quest.Where(q => q.scriptableQuest != null && q.scriptableQuest.idQuest == currentStage);
    // }

    public void FirstButton()
    {
        SelectButton(0);
        UpdateButtonPage();
    }

    public void OpenMenu()
    {
        FirstButton();
        _isPause = true;
        Time.timeScale = 0f;
        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void CloseMenu()
    {
        _isPause = false;
        Time.timeScale = 1f;
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void ButtonFromQuestLog()
    {
        quest = UIQuestLog.instance.GetQuestInfos();
    }

    public void UpdateButtonPage() 
    {
        foreach (var questButton in quest) 
        {
            if (questButton != null) { 
                this.buttonSelected.buttonPage.Add(questButton.gameObject);
            }
        }

        // FirstButton();
    }

#region 
#if OLD
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
        // HintManager.instance.ShowHint(hint);
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

#endif
#endregion
}
