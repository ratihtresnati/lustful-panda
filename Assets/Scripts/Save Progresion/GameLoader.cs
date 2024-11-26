using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public Button loadButton; 
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        loadButton.onClick.AddListener(LoadGame);
    } 

    public void LoadGame()
    {
        playerManager.LoadPlayerData();  
        Debug.Log("Game loaded.");
    }
}
