using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystemJSON : MonoBehaviour
{
    public Transform player; 
    private string saveFilePath;

    private void Start()
    {
        
        saveFilePath = Application.persistentDataPath + "/savegame.json";
    }

   
    public void SaveGame()
    {
        SaveData data = new SaveData
        {
            playerX = player.position.x,
            playerY = player.position.y,
            playerZ = player.position.z,
        };

        
        string json = JsonUtility.ToJson(data, true); // true = format indented (rapi)
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved! File path: " + saveFilePath);
    }

    
    public void LoadGame()
    {
       
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            
            player.position = new Vector3(data.playerX, data.playerY, data.playerZ);

            Debug.Log("Game Loaded! Data: " + json);
        }
        else
        {
            Debug.LogWarning("No save file found at: " + saveFilePath);
        }
    }
}
