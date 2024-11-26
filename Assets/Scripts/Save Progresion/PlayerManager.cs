using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int currentQuestID;      
    public int currentStage = 1;    
    public bool hintDisplayed;      

    private void Start()
    {
        LoadPlayerData(); 
    }

    
    public void OnStageChange()
    {
        SavePlayerData();
    }

    private void SavePlayerData()
    {
        PlayerData data = new PlayerData
        {
            playerPosition = transform.position, 
            currentQuestID = currentQuestID,     
            currentStage = currentStage,         
            hintDisplayed = hintDisplayed        
        };
        SaveSystem.SavePlayerData(data);         
    }

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayerData(); 
        if (data != null)
        {
            transform.position = data.playerPosition; 
            currentQuestID = data.currentQuestID;     
            currentStage = data.currentStage;         
            hintDisplayed = data.hintDisplayed;       
        }
        else
        {
            Debug.Log("Memulai game baru...");
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StageExit"))
        {
            Debug.Log("Stage " + currentStage + " selesai.");
            currentStage++;       
            OnStageChange();      
        }
    }

    
    public void AdvanceToNextQuest()
    {
        currentQuestID++;         
        SavePlayerData();         
        Debug.Log("Quest " + currentQuestID + " diselesaikan.");
    }
}