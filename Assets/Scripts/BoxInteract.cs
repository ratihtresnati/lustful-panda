using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : MonoBehaviour
{
    public PlayerController PlayerController;
    public GameObject InterectDialogue;

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerController.InBox)
        {
            InterectDialogue.SetActive(true);
            if (InputManager.instance.InteractInput)
            {
                StartCoroutine(BecomeBox());
            }
        }
        else
        {
            InterectDialogue.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InterectDialogue.SetActive(false);
    }

    IEnumerator BecomeBox()
    {
        PlayerController.SmokeVFX.Play();
        yield return new WaitForSeconds(0.5f);
        PlayerController.InBox = true;
    }
}
