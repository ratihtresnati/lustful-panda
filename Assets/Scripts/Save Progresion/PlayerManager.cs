using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int currentQuestID;  // ID quest yang sedang dikerjakan
    public bool hintDisplayed;  // Status hint

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
            hintDisplayed = hintDisplayed
        };
        SaveSystem.SavePlayerData(data);
    }

    private void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        if (data != null)
        {
            transform.position = data.playerPosition;
            currentQuestID = data.currentQuestID;
            hintDisplayed = data.hintDisplayed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StageExit"))
        {
            OnStageChange();
        }
    }
}