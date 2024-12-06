using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractQuest : MonoBehaviour
{
    private conditionalObjectInteract _conditionalObject; 

    private void Start()
    {
        _conditionalObject = GetComponent<conditionalObjectInteract>();
    }

    public bool Interact()
    {
        return _conditionalObject.IsInteract;
    }

    public bool GiveItem()
    {
        return _conditionalObject.IsGiveItem;
    }
}
