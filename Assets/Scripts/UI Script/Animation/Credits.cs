using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public RectTransform creditText;
    public float duration = 2f;
    public GameObject Button;
    private bool inputReceived = false;
    SceneManager sceneManager;
    private void Start()
    {
       creditText.anchoredPosition = new Vector2(0, -Screen.height);
        creditText.DOAnchorPosY(Screen.height + 1400, duration).SetEase(Ease.Linear).OnComplete(() => 
        {
            Loading.instance.LoadScene(0);
            DOTween.Kill(creditText);
        });

        if (Button != null)
        {
            Button.SetActive(false);
        }
    }
    void Update()
    {
        // if (!inputReceived && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        // {
        //     inputReceived = true;
        //     ShowButton();
        // }
    }
    void ShowButton()
    {
        if (Button != null)
        {
            Button.SetActive(true);
        }
    }
    public void Continue()
    {
        Loading.instance.LoadScene(0);
    }
}
