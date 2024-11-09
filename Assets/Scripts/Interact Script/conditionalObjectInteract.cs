using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class conditionalObjectInteract : MonoBehaviour
{
    private PlayerInputManager playarInputManager;

    public float interactionRadius;
    public GameObject player;
    public GameObject taskItem;
    public GameObject dialogAsset;
    private bool isCarryingTheItem = false;
    [SerializeField] private Outline _outline;
    [SerializeField] private Outline _itemOutline;
    public int questNum;
    
    private PlayerHoldPosition playerHoldPosition;
    private ParentPosition _parentPosition;


    void Start()
    {
        playerHoldPosition = FindObjectOfType<PlayerHoldPosition>();
        _outline = gameObject.GetComponent<Outline>();
        _itemOutline = taskItem.GetComponent<Outline>();
        _parentPosition = gameObject.GetComponent<ParentPosition>();

        if (dialogAsset != null)
        {
            dialogAsset.SetActive(false);
        }
        player = GameObject.Find("Panda Bayik");
    }
    void Update()
    {
        // Check bawaan item
        if (taskItem != null && taskItem.transform.parent == playerHoldPosition.PositionParent())
        {
            isCarryingTheItem = true;
        }
        else
        {
            isCarryingTheItem = false;
        }
        // Menghitung jarak pemain n objek
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionRadius && InputManager.instance.InteractInput)
        {
            if (isCarryingTheItem)
            {
                Interact();
            }
            else
            {
                if(_itemOutline != null)
                {
                    _itemOutline.ApplyOutline(true);
                }   
                
                dialogAsset.transform.SetParent(_parentPosition.PositionParent());
                dialogAsset.SetActive(true);
                QuestManager.instance.NextQuest();
            }
        }
    }

    /*private void InteractEvent(InputAction.CallbackContext context)
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionRadius)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= interactionRadius)
            {
                if (isCarryingTheItem)
                {
                    Interact();
                }
                else
                {
                    _itemOutline.ApplyOutline(true);
                    dialogAsset.SetActive(true);
                    QuestManager.instance.NextQuest();
                }
            }
        }
    }*/

    void Interact()
    {
        switch (questNum) {
        case 1: //npc panda
            NPCPandaStateController npcPanda = GetComponent<NPCPandaStateController>();
            if (npcPanda != null)
            {
                npcPanda._isComplete = true;
                QuestManager.instance.NextQuest();
                _outline.ApplyOutline(false);
            }
            else
            {
                Debug.LogWarning("NPCPandaStateController tidak ditemukan pada objek ini.");
            }
        break;
        case 2: //final door
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null){
                boxCollider.enabled = false;
            }
        break;
        }
        Destroy(taskItem);
        Destroy(dialogAsset);
    }
}