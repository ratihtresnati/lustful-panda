using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionalObjectInteract : MonoBehaviour
{
    public float interactionRadius; // Radius interaksi
    public GameObject player;
    public GameObject taskItem; // Referensi ke item quest
    public GameObject dialogAsset;
    private bool isCarryingTheItem = false;
    [SerializeField] private Outline _outline;

    [SerializeField] private Outline itemOutline;
    public int questNum;
    
    private PlayerHoldPosition playerHoldPosition;

    void Start()
    {
        playerHoldPosition = FindObjectOfType<PlayerHoldPosition>();
        _outline = GetComponent<Outline>();
         if (dialogAsset != null)
        {
            dialogAsset.SetActive(false);
        }

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
        if (distance <= interactionRadius && Input.GetKeyDown(KeyCode.F))
        {
            if (isCarryingTheItem)
            {
                Interact();
            }
            else
            {
                itemOutline.ApplyOutline(true);
                dialogAsset.SetActive(true);
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
                QuestManager.instance._questIsComplete = true;
                if (_outline != null){
                    _outline.ApplyOutline(false);
                    }
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