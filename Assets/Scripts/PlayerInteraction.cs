using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2.0f;
    public Transform interactableObject;
    private bool hasGivenQuest = false;
    public bool isFirstQuest = true;

    void Start()
    {
        if (isFirstQuest && !hasGivenQuest)
        {
            GiveInitialQuest();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckForInteraction();
        }

        // Cek apakah quest pertama sudah selesai, jika ya, berikan quest berikutnya
        if (!isFirstQuest && hasGivenQuest && QuestManager.instance.AllQuestsCompleted())
        {
            GiveNextQuest();
            hasGivenQuest = false; // Reset agar quest berikutnya bisa diberikan
        }
    }

    void CheckForInteraction()
    {
        if (interactableObject != null)
        {
            float distance = Vector3.Distance(transform.position, interactableObject.position);
            Debug.DrawLine(transform.position, interactableObject.position, Color.red, 1.0f);

            if (distance <= interactionRange)
            {
                GiveQuest();
            }
            else if (distance > interactionRange)
            {
                Debug.Log("Object di luar jangkauan interaksi.");
            }
        }
        else
        {
            Debug.LogError("interactable Object belum dihubungkan di Inspector!");
        }
    }

    void GiveQuest()
    {
        if (!hasGivenQuest && QuestManager.instance != null && HintManager.instance != null)
        {
            // Logika quest pertama
        }
        else
        {
            HintManager.instance.ShowHint("Pintu ini terkunci, coba cari kunci di sekitar ruangan.");
        }
    }

    void GiveInitialQuest()
    {
        if (QuestManager.instance != null && HintManager.instance != null)
        {
            Quest initialQuest = new Quest("Quest Awal", "Temukan kunci pembuka pintu", "Kunci");
            QuestManager.instance.AddQuest(initialQuest);
            hasGivenQuest = true;
            isFirstQuest = false;
            HintManager.instance.ShowHint("Temukan kunci agar kamu keluar dari ruangan ini.");
        }
        else
        {
            Debug.LogError("Instance QuestManager atau HintManager tidak ditemukan!");
        }
    }

    void GiveNextQuest()
    {
        if (QuestManager.instance != null && HintManager.instance != null)
        {
            Quest nextQuest = new Quest("Quest Berikutnya", "Temukan Objek Rahasia di dalam ruangan", "ObjekRahasia");
            QuestManager.instance.AddQuest(nextQuest);
            HintManager.instance.ShowHint("Ada sesuatu yang misterius di ruangan ini. Coba temukan objek rahasia untuk melanjutkan!");
        }
        else
        {
            Debug.LogError("Instance QuestManager atau HintManager tidak ditemukan!");
        }
    }
}
