using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaInteractMultiple : MonoBehaviour
{
    public float interactDistance = 5f; 
    public Transform panda; 
    public float pushForce = 5f; 

    void Update()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(panda.position, interactDistance);
        
        Debug.Log("Total objek terdeteksi: " + hitColliders.Length); 
        
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Collider hitCollider in hitColliders)
            {
                
                if (hitCollider.CompareTag("Interactable"))
                {
                    Debug.Log("Objek terdeteksi: " + hitCollider.gameObject.name); // Debug: Nama objek terdeteksi
                    PushObject(hitCollider.gameObject);
                }
            }
        }
    }

    void PushObject(GameObject obj)
    {
        // Mendorong objek dengan force pelan
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Dorongan ke depan relatif terhadap posisi panda
            Vector3 pushDirection = (obj.transform.position - panda.position).normalized;
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            Debug.Log("Objek didorong: " + obj.name); // Debug: Objek didorong
        }
        else
        {
            Debug.LogWarning("Objek tidak memiliki komponen Rigidbody: " + obj.name);
        }
    }
}
