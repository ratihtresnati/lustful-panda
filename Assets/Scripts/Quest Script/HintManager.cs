using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    public static HintManager instance;
    public TMP_Text hintText; 
    public float hintDisplayTime = 5.0f;  

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    public void ShowHint(string hint)
    {
        hintText.text = hint;
        // StartCoroutine(HideHintAfterDelay());
    }

    
    private IEnumerator HideHintAfterDelay()
    {
        yield return new WaitForSeconds(hintDisplayTime);
        hintText.text = "";
    }
}
