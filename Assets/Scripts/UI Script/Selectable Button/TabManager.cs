using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public SelectButtonHandler selectButtonHandler;

    private void Update()
    {
        selectButtonHandler = FindObjectOfType<SelectButtonHandler>();
        if (selectButtonHandler != null)
        {
            Debug.Log(" " + selectButtonHandler.gameObject.name);
        }
    }
}
