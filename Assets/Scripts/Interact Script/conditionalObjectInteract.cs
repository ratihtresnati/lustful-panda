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
        if (taskItem.transform.parent == player.transform)
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
            if(isCarryingTheItem){
                Interact();
            }
            else{
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

        NPCPandaStateController npcPanda = GetComponent<NPCPandaStateController>();
        npcPanda._isComplete = true;
        QuestManager.instance._questIsComplete = true;
        if(_outline != null)
        {
            _outline.ApplyOutline(false);
        }

        Destroy(taskItem);
        Destroy(dialogAsset);
    }
}