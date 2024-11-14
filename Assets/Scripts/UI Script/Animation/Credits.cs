using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Credits : MonoBehaviour
{
    public RectTransform creditText;
    public float duration = 2f;
    private void Start()
    {
       creditText.anchoredPosition = new Vector2(0, -Screen.height);
        creditText.DOAnchorPosY(Screen.height, duration).SetEase(Ease.Linear).OnComplete(() => 
        {
            Loading.instance.LoadScene(0);
            DOTween.Kill(creditText);
        });
    }


}
