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
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name 
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

            
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != data.sceneName)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(data.sceneName);
            }

            
            StartCoroutine(SetPlayerPositionAfterSceneLoad(data));
        }
        else
        {
            Debug.LogError("Save file not found at " + saveFilePath);
        }
    }

    private IEnumerator SetPlayerPositionAfterSceneLoad(SaveData data)
    {
        
        yield return new WaitUntil(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == data.sceneName);

        
        GameObject panda = GameObject.Find("Player");
        if (panda != null)
        {
            panda.transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
            Debug.Log("Game Loaded! Panda position: " + panda.transform.position);
        }
        else
        {
            Debug.LogError("Player object not found in the new scene!");
        }
    }
}
