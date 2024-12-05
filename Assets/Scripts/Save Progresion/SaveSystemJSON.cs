using System.Collections; 
using System.IO; 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystemJSON : MonoBehaviour
{
    public static SaveSystemJSON Instance; 
    public Transform player; 
    private string saveFilePath;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

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
            sceneName = SceneManager.GetActiveScene().name 
        };

        string json = JsonUtility.ToJson(data, true); 
        File.WriteAllText(saveFilePath, json);

        Debug.Log($"Game Saved! Panda position: {player.position.x}, {player.position.y}, {player.position.z}, Scene: {SceneManager.GetActiveScene().name}");
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log($"Loaded Data: {json}");

            
            SceneManager.LoadScene(data.sceneName); 

           
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogError("Save file not found at " + saveFilePath);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
        StartCoroutine(SetPlayerPositionAfterSceneLoad());
    }

    private IEnumerator SetPlayerPositionAfterSceneLoad()
    {
        yield return new WaitForSeconds(0.1f); 

        string json = File.ReadAllText(saveFilePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        GameObject panda = GameObject.FindWithTag("PandaMC");
        if (panda != null)
        {
            panda.transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
            Debug.Log($"Panda moved to saved position: {panda.transform.position}");
        }
        else
        {
            Debug.LogError("Player object not found in the new scene!");
        }
    }
}
