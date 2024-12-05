using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuest : MonoBehaviour
{
    // private conditionalObjectInteract _conditionalObject; 

    // private void Start()
    // {
    //     _conditionalObject = GetComponent<conditionalObjectInteract>();
    // }

    // public bool QuestComplete()
    // {
    //     return _conditionalObject.IsGiveItem;
    // }

    private interactItem _interactItem; 

    private void Start()
    {
        _interactItem = GetComponent<interactItem>();
    }

    public bool QuestComplete()
    {
        return _interactItem.IsTakeItems;
    }
}
