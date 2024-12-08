using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : MonoBehaviour
{
    public PlayerController PlayerController;

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerController.InBox) 
        { 
            if (InputManager.instance.InteractInput)
            {
                StartCoroutine(BecomeBox());
            }
        }
    }

    IEnumerator BecomeBox()
    {
        PlayerController.SmokeVFX.Play();
        yield return new WaitForSeconds(0.5f);
        PlayerController.InBox = true;
    }
}
