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

    public bool IsInteract { get; private set; }
    public bool IsGiveItem { get; private set; }


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

        IsInteract = false;
        IsGiveItem = false;
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
            if (isCarryingTheItem == true && IsInteract == true)
            {
                Interact();
            }
            else if (isCarryingTheItem == false)
            {
                if(_itemOutline != null)
                {
                    _itemOutline.ApplyOutline(true);
                }   
                
                dialogAsset.transform.SetParent(_parentPosition.PositionParent());
                dialogAsset.SetActive(true);
                IsInteract = true;
            }
        }
    }

    void Interact()
    {
        IsGiveItem = true;
        NPCPandaStateController npcPanda = GetComponent<NPCPandaStateController>();
        if (npcPanda != null)
        {
            npcPanda._isComplete = true;
            _outline.ApplyOutline(false);
        }
        else
        {
            Debug.LogWarning("NPCPandaStateController tidak ditemukan pada objek ini.");
        }

        Destroy(taskItem);
        Destroy(dialogAsset);
    }
    
    
}