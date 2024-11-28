using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import untuk TextMeshPro

public class NavigationUiFadeOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup navigationUI;
    [SerializeField] private TextMeshProUGUI infoText; // Teks yang muncul
    private bool isFading = false;

    void Start()
    {
        // Pastikan teks terlihat di awal
        infoText.alpha = 1f;
    }

    void Update()
    {
        // Check for any key or mouse button press
        if (!isFading && Input.anyKeyDown)
        {
            StartCoroutine(CanvasFadeOut());
        }
    }

    IEnumerator CanvasFadeOut()
    {
        isFading = true; // Prevent multiple coroutines
        yield return new WaitForSeconds(0.5f);

        while (navigationUI.alpha > 0)
        {
            // Gradually decrease alpha of both UI and text
            navigationUI.alpha -= Time.deltaTime;
            infoText.alpha -= Time.deltaTime;
            yield return null; // Wait for next frame
        }

        navigationUI.alpha = 0; // Ensure alpha is fully 0
        infoText.alpha = 0;    // Ensure text is fully 0
    }
}