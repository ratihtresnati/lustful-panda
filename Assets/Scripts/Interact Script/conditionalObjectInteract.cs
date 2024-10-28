using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionalObjectInteract : MonoBehaviour
{
    public float interactionRadius; // Radius interaksi
    public GameObject player;
    public GameObject taskItem; // Referensi ke item quest
    private bool isCarryingTheItem = false;
    [SerializeField] private Outline _outline;
    private PlayerHoldPosition playerHoldPosition;

    void Start()
    {
        playerHoldPosition = FindObjectOfType<PlayerHoldPosition>();
        _outline = GetComponent<Outline>();
    }

    void Update()
    {
        // Check bawaan item
        if (taskItem != null)
        {
            if (taskItem.transform.parent == playerHoldPosition.PositionParent())
            {
                isCarryingTheItem = true;
            }
            else
            {
                isCarryingTheItem = false;
            }
        }

        // Menghitung jarak pemain n objek
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionRadius && isCarryingTheItem && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("interaksi objek berhasil");

        NPCPandaStateController npcPanda = GetComponent<NPCPandaStateController>();
        npcPanda._isComplete = true;
        QuestManager.instance._questIsComplete = true;
        if(_outline != null)
        {
            _outline.ApplyOutline(false);
        }

        Destroy(taskItem);
        // Destroy(gameObject);
    }
}