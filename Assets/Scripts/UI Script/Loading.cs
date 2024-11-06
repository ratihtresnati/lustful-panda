using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public static Loading instance;
    public GameObject LoadingScreen;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene (int i )
    {
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

        if (LoadingScreen != null)
        {
            LoadingScreen.SetActive(false);
        }
    }
}
