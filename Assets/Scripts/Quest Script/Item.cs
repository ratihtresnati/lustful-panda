using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;  

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player menemukan: " + itemName);
            QuestManager.instance.ItemCollected(itemName);  
            Destroy(gameObject);  
        }
    }
}
