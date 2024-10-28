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
    [SerializeField] private ObjectInfo ObjectInput;

    public enum ObjectInfo {
        PandaNPC,
        FinalDoor
    }

    // Variabel penyimpanan internal untuk ObjectQuest
    private ObjectInfo _objectQuest;

    public ObjectInfo ObjectQuest {
        get { return _objectQuest; }
        set { _objectQuest = value; }
    }

    void Start()
    {
        _outline = GetComponent<Outline>();

        if (dialogAsset != null)
        {
            dialogAsset.SetActive(false);
        }
    }

    void Update()
    {
        // Check bawaan item
        if (taskItem != null && taskItem.transform.parent == player.transform)
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
                if (dialogAsset != null)
                {
                    dialogAsset.SetActive(true);
                }
            }
        }
    }

    void Interact()
    {
        Debug.Log("interaksi objek berhasil");

        switch (ObjectQuest)
    {
        case ObjectInfo.PandaNPC:
            // Only attempt to get the component if it exists
            NPCPandaStateController npcPanda = GetComponent<NPCPandaStateController>();
            if (npcPanda != null)
            {
                npcPanda._isComplete = true;
                QuestManager.instance._questIsComplete = true;
                if (_outline != null)
                {
                    _outline.ApplyOutline(false);
                }
            }
            else
            {
                Debug.LogWarning("NPCPandaStateController tidak ditemukan pada objek ini.");
            }
            break;

        case ObjectInfo.FinalDoor:
            // Ensure collider is disabled only for the FinalDoor case
            Collider objectCollider = GetComponent<Collider>();
            if (objectCollider != null)
            {
                objectCollider.enabled = false;
                Debug.Log("Collider pada FinalDoor berhasil dimatikan.");
            }
            else
            {
                Debug.LogWarning("Collider tidak ditemukan pada objek ini.");
            }
            break;
    }

        Destroy(taskItem);
        Destroy(dialogAsset);
    }
}