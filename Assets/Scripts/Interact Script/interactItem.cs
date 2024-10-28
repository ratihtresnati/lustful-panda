using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interactItem : MonoBehaviour
{
    public float pickupRadius;
    private bool canPickup = false;
    bool isCarryingItem = false;
    public GameObject player;
    public GameObject obstacleObject; 
    public Vector3 grabOffsetPlayer; // jarak objek setelah diambil karakter
    public Vector3 dropOffsetPlayer; // jarak objek setelah ditaro karakter
    public Outline outline;

    [SerializeField] private UnityEvent _nextObject;

    void Start()
    {

    }

    void Update()
    {
        // Menghitung jarak antara pemain dan objek
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Mengecek apakah jarak pemain kurang dari radius pickup
        if (distance <= pickupRadius)
        {
            if (obstacleObject != null)
            {
                Debug.Log("belum bisa interact");
                return;
            }
            else
            {
                canPickup = true;
                Debug.Log("ambil barang = E");
            }
        }
        else
        {
            canPickup = false;
        }

        // Ambil item
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (!isCarryingItem)
            {
                Pickup();
            }
        }

        // Drop item
        if (isCarryingItem && Input.GetKeyDown(KeyCode.R))
        {
            Drop();
        }
    }

    void Pickup()
    {
        _nextObject.Invoke();
        Debug.Log("membawa barang");

        // Matiin outline
        if (outline != null)
        {
            outline.ApplyOutline(false);
        }

        // Positioning item
        transform.position = player.transform.TransformPoint(grabOffsetPlayer);
        float yPlayer = player.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yPlayer, 0);

        // Menjadikan pemain parent, biar nempel
        transform.SetParent(player.transform);

        isCarryingItem = true;
        canPickup = false;
    }

    void Drop()
    {
        Debug.Log("barang dilepas");

        // Nyalain outline
        if (outline != null)
        {
            outline.ApplyOutline(true);
        }

        // Positioning item
        transform.position = player.transform.TransformPoint(dropOffsetPlayer);
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        // Lepas parent
        transform.SetParent(null);

        isCarryingItem = false;
        canPickup = true;
    }
}