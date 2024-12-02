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
        string path = Application.persistentDataPath + "/savegame.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            
            GameObject panda = GameObject.Find("Player");
            if (panda != null)
            {
                
                panda.transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);

                Debug.Log("Game Loaded! Panda position: " + panda.transform.position);
            }
            else
            {
                Debug.LogError("Panda object not found in scene!");
            }
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
        }
    }
}
