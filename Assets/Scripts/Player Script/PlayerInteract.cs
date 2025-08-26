using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 5f; // Jarak di mana panda bisa berinteraksi dengan objek
    public Rig boxAnim;
    public bool PushBox { get; private set; }
    [SerializeField] private float _pushForce = 5f; // Kekuatan dorongan pada objek
    [SerializeField] private float _duration = 2f;
    private Transform _panda; // Referensi ke posisi panda
    private GameObject _currentObject;
    private bool _findInteractable = false;
    private bool _isPlayed = false;
    [SerializeField] bool keyframe;

    private void Start()
    {
        _panda = gameObject.transform;
    }

    void Update()
    {
        if (keyframe)
        {
            boxAnim.weight = 0f; 
        }
        
        if (PushBox == false)
        {
            boxAnim.weight -= Time.deltaTime * _duration;
            _isPlayed = false;
        }
        else
        {
            boxAnim.weight += Time.deltaTime * _duration;
            if (_isPlayed == false)
            {
                AudioManager.Instance.Play("DorongBox");
                _isPlayed = true;
            }
        }

        // Mendeteksi jika ada beberapa box di sekitar _panda
        Collider[] hitColliders = Physics.OverlapSphere(_panda.position, interactDistance);
        _findInteractable = false;
    
        // Cek apakah objek memiliki tag "Interactable"
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Interactable"))
            {
                _findInteractable = true;
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                float distance = Vector3.Distance(_panda.position, hitCollider.transform.position);

                if (InputManager.instance.InteractInputDown)
                {
                    PushBox = true;
                    _currentObject = hitCollider.gameObject; 
                    PushObject(_currentObject);
                }
                else if (rb != null && rb.isKinematic == false)
                {
                    PushBox = false;
                    rb.isKinematic = true;
                }
            } 
        }

        // Debug.Log(_isPlayed);

        if (_findInteractable == false) 
        {
            PushBox = false;
            if (_currentObject != null)
            {
                Rigidbody rb = _currentObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Set kinematic
                }
                _currentObject = null; // Reset objek yang sedang didorong
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
            // Dorongan ke depan relatif terhadap posisi _panda
            Vector3 pushDirection = (obj.transform.position - _panda.position).normalized;
            rb.AddForce(pushDirection * _pushForce, ForceMode.Impulse);
            Debug.Log("Objek didorong: " + obj.name); // Debug: Objek didorong
        }
        else
        {
            Debug.LogWarning("Objek tidak memiliki komponen Rigidbody: " + obj.name);
        }
    }
}
