using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public static TabManager instance;
    public SelectButtonHandler selectButtonHandler;

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

    private void Update()
    {
        SelectButtonHandler[] handlers = FindObjectsOfType<SelectButtonHandler>();

        if (handlers.Length > 0)
        {
            selectButtonHandler = null;

            for (int i = handlers.Length - 1; i >= 0; i--)
            {
                if (handlers[i].gameObject.name != "Settings Menu")
                {
                    selectButtonHandler = handlers[i];
                    break; 
                }
            }
        }
        else
        {
            selectButtonHandler = null;
        }
    }
}
