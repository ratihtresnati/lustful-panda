using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationUiFadeOut : MonoBehaviour
{

    [SerializeField] private CanvasGroup navigationUI;

    IEnumerator CanvasFadeOut()
    {
        yield return new WaitForSeconds(2);

        navigationUI.alpha -= Time.deltaTime;

    }
    
    private void Update()
    {
        StartCoroutine(CanvasFadeOut());
    }
}
