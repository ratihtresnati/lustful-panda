using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public static Loading instance;
    public GameObject LoadingScreen;
    public bool IsLoading { get; private set; }
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
    }

    public void LoadScene (int i )
    {
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
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }

        IsLoading = false;
        LoadingScreen.SetActive(false);
    }
}
