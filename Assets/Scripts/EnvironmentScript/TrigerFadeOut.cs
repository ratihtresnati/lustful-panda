using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerFadeOut : MonoBehaviour
{
    [SerializeField] FadeOut fadeOut;

   

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("PandaMC") || other.CompareTag("PandaRolling"))
        {
            fadeOut.DoFade = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PandaMC") || other.CompareTag("PandaRolling"))
        {
            fadeOut.DoFade = false;
        }
    }
}
