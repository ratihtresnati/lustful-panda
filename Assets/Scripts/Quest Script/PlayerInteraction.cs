using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2.0f;  
    public Transform interactableObject;  
    private bool hasGivenQuest = false;
    public bool isFirstQuest = true; 

    // void Start()
    // {
        
    //     if (isFirstQuest && !hasGivenQuest)
    //     {
    //         GiveInitialQuest();
    //     }
    // }  

    void Update()
    {
        
        if(Input.GetKey(KeyCode.Q))
        {
            QuestManager.instance._questIsComplete = true;
        }

    }

    // private void ResetQuest()
    // {
    //     QuestManager.instance.ResetQuest();

    //     if
    // }

    //     if (Input.GetKeyDown(KeyCode.Z))
    //     {
    //         CheckForInteraction();
    //     }
    // }

    // void CheckForInteraction()
    // {
        
    //     float distance = Vector3.Distance(transform.position, interactableObject.position);

    //     if (distance <= interactionRange && !hasGivenQuest)
    //     {
    //         InteractWithObject();  
    //     }
    //     else if (distance > interactionRange)
    //     {
    //         Debug.Log("Object tidak terdeteksi");
    //     }
    // }

    // void InteractWithObject()
    // {
    //     Debug.Log("Interaksi dengan object");
    //     GiveQuest();
    // }

    // void GiveQuest()
    // {
    //     if (!hasGivenQuest)
    //     {
            
    //         Quest newQuest = new Quest("Ambil Bambo", "Ambil bambo di dekat tempat makan", "Bamboo");
    //         QuestManager.instance.AddQuest(newQuest);
    //         hasGivenQuest = true;  
    //         HintManager.instance.ShowHint("Cari kunci di dekat ruangan untuk membuka pintu.");
    //     }
    //     else
    //     {
    //         Debug.Log("Tidak ada Quest");
    //     }
    // }

    // void GiveInitialQuest()
    // {
    //     Quest initialQuest = new Quest("Tutorial Rolling", "Gunakan Q untuk berguling", "Bamboo");
    //     QuestManager.instance.AddQuest(initialQuest);
    //     hasGivenQuest = true;
    //     isFirstQuest = false;
    //     HintManager.instance.ShowHint("Mulailah dengan mencari jalan keluar.");
    // }
}
