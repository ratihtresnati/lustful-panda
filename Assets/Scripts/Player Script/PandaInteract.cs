using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaInteractMultiple : MonoBehaviour
{
    public float interactDistance = 5f; // Jarak di mana panda bisa berinteraksi dengan objek
    public Transform panda; // Referensi ke posisi panda
    public float pushForce = 5f; // Kekuatan dorongan pada objek

    void Update()
    {
        // Mendeteksi jika ada beberapa box di sekitar panda
        Collider[] hitColliders = Physics.OverlapSphere(panda.position, interactDistance);
        
        Debug.Log("Total objek terdeteksi: " + hitColliders.Length); // Debug: Tampilkan jumlah objek yang terdeteksi
        
        // Hanya lanjutkan jika tombol E ditekan
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Collider hitCollider in hitColliders)
            {
                // Cek apakah objek memiliki tag "Interactable"
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
            rb.isKinematic = false;
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
