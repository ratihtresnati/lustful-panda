using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    [SerializeField] private GameObject InterectDialogue;


    private void Start()
    {
        InterectDialogue.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        InterectDialogue.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        InterectDialogue.SetActive(false);
    }
}
