using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : MonoBehaviour
{
    public PlayerController PlayerController;

    private void OnTriggerStay(Collider other)
    {
        if (InputManager.instance.ButtonClickInput)
        {
            PlayerController.InBox = true;
            //Debug.Log("Getbox");
        }
    }

}
