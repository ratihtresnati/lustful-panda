using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public static Loading instance;
    public GameObject LoadingScreen;
    public bool IsLoading;
    private SaveSystemJSON saveSystem; // Tambahkan referensi untuk SaveSystemJSON
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }

        Debug.Log(IsLoading);
        saveSystem = GameObject.FindObjectOfType<SaveSystemJSON>(); // Temukan SaveSystemJSON
    }

    public void LoadScene (int i )
    {
        // Muat game data sebelum scene dimuat
        if (saveSystem != null)
        {
            Debug.Log("Load game data.");
            saveSystem.LoadGame();  // Muat game
        }
        else
        {
            Debug.LogError("SaveSystemJSON not found! Cannot load saved data.");
        }

        // InputManager.PlayerInput.enabled = false; 
        IsLoading = true;
        StartCoroutine(LoadSceneAsync(i));
    }

    IEnumerator LoadSceneAsync(int i)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(i);

        LoadingScreen.SetActive(true);
        

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.2f);

            yield return null;
        }
        
        IsLoading = false;
        LoadingScreen.SetActive(false);
    }
}
