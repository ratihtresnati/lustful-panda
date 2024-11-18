using System;
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
    public GameObject destinedObject;
    public GameObject obstacleObject; 
    // public Vector3 grabOffsetPlayer; // jarak objek setelah diambil karakter
    public Vector3 dropOffsetPosPlayer; // jarak objek setelah ditaro karakter
    public Vector3 dropOffsetRotPlayer; // rotasi objek setelah ditaro karakter
    public Outline outline;
    private PlayerHoldPosition _playerHoldPosition;

    void Start()
    {
        player = GameObject.Find("Panda Bayik");
        _playerHoldPosition = player.GetComponent<PlayerHoldPosition>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        float distance2 = Vector3.Distance(player.transform.position, destinedObject.transform.position);

        // ngecek jarak pemain di debug
        if (distance <= pickupRadius){
            if (obstacleObject != null)
            {return;}
            else{
                canPickup = true;}
        }
        else{
            canPickup = false;
        }

        // interact item
        if(InputManager.instance.InteractInput){
            if (canPickup && !isCarryingItem){
                Pickup();
            }
            else if (isCarryingItem && distance2 <= pickupRadius){
                
            }
            else if (isCarryingItem){
                Drop();
            }
        }
    }
    void Pickup()
    {
        if (outline != null){
            outline.ApplyOutline(false);
        }

        transform.parent = _playerHoldPosition.PositionParent();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 90, 0);

        isCarryingItem = true;
        canPickup = false;
    }
    void Drop()
    {
        if (outline != null){
            outline.ApplyOutline(true);
        }

        // Positioning item
        transform.position = player.transform.TransformPoint(dropOffsetPosPlayer);
        transform.localRotation = Quaternion.Euler(dropOffsetRotPlayer);

        // Lepas parent
        transform.SetParent(null);

        isCarryingItem = false;
        canPickup = true;
    }
}