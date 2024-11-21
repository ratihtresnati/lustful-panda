using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAudio : MonoBehaviour
{
    public AudioClip[] footstepsSound;
    public AudioSource characterAudio;

    public void PlayFootstep()
    {
        characterAudio.PlayOneShot(footstepsSound[Random.Range(0,footstepsSound.Length)]);
    }
}
