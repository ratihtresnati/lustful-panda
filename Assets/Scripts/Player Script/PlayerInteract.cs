using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerInteract : MonoBehaviour
{    
    public float interactDistance = 5f; // Jarak di mana panda bisa berinteraksi dengan objek
    public Transform panda; // Referensi ke posisi panda
    public float pushForce = 5f; // Kekuatan dorongan pada objek


    public Animator _animator;
    public Rig boxAnim;
    public bool PushBox { get; private set; }
    private GameObject currentObject;
    public bool _findInteractable = false;
    [SerializeField] private float duration = 0.3f;


    private void Start()
    {
        panda = gameObject.transform;
        _animator = panda.GetComponent<Animator>();
    }

    void Update()
    {
        if (PushBox == false)
        {
            boxAnim.weight -= Time.deltaTime * duration;
        }else
        {
            boxAnim.weight += Time.deltaTime * duration;
        }

        // Mendeteksi jika ada beberapa box di sekitar panda
        Collider[] hitColliders = Physics.OverlapSphere(panda.position, interactDistance);
        _findInteractable = false;
    
        // Cek apakah objek memiliki tag "Interactable"
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Interactable"))
            {
                _findInteractable = true;
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                float distance = Vector3.Distance(panda.position, hitCollider.transform.position);

                Debug.Log(distance);

                if (InputManager.instance.InteractInputDown)
                {
                    // Debug.Log("Objek terdeteksi: " + hitCollider.gameObject.name); // Debug: Nama objek terdeteksi
                    PushBox = true;
                    currentObject = hitCollider.gameObject; 
                    PushObject(currentObject);
                }
                else if (rb != null && rb.isKinematic == false)
                {
                    PushBox = false;
                    rb.isKinematic = true;
                }
            } 
        }

        if (_findInteractable == false) 
        {
            PushBox = false;
            if (currentObject != null)
            {
                Rigidbody rb = currentObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Set kinematic
                }
                currentObject = null; // Reset objek yang sedang didorong
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
