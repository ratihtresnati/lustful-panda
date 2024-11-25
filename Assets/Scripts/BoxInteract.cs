using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : MonoBehaviour
{
    public PlayerController PlayerController;

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            PlayerController.InBox = true;
            //Debug.Log("Getbox");
        }
    }

}
