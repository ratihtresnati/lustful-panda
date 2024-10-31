using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class conditionalObjectInteract : MonoBehaviour
{
    private PlayerInputManager playarInputManager;

    public float interactionRadius; // Radius interaksi
    public GameObject player;
    public GameObject taskItem; // Referensi ke item quest
    public GameObject dialogAsset;
    private bool isCarryingTheItem = false;
    [SerializeField] private Outline _outline;
    [SerializeField] private Outline _itemOutline;
    public int questNum;
    
    private PlayerHoldPosition playerHoldPosition;

    void Start()
    {
        playerHoldPosition = FindObjectOfType<PlayerHoldPosition>();
        _outline = gameObject.GetComponent<Outline>();
        _itemOutline = taskItem.GetComponent<Outline>();
         if (dialogAsset != null)
        {
            dialogAsset.SetActive(false);
        }
        player = GameObject.Find("Panda Bayik");

        // Input System
        playarInputManager = new PlayerInputManager();
        playarInputManager.Player.Enable(); //Mengaktifkan control mapping
        playarInputManager.Player.Interact.performed += InteractEvent; //Mengaktifkan Action Interect
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
        /*
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionRadius && Input.GetKeyDown(KeyCode.E))
        {
            if (isCarryingTheItem)
            {
                Interact();
            }
            else
            {
                itemOutline.ApplyOutline(true);
                dialogAsset.SetActive(true);
                QuestManager.instance.NextQuest();
            }
        }
        */
    }

    // Menghitung jarak pemain n objek
    private void InteractEvent(InputAction.CallbackContext context)
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