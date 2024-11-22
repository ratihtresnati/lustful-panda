using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    public AudioClip[] footstepsSound;
    public AudioClip endJumpSound;
    public AudioClip rollingSound;

    public AudioSource characterAudio;

    public void PlayFootstep()
    {
        characterAudio.PlayOneShot(footstepsSound[Random.Range(0,footstepsSound.Length)]);
    }

    public void PlayRolling()
    {
        characterAudio.PlayOneShot(rollingSound);
    }

    public void PlayEndJump()
    {
        characterAudio.PlayOneShot(endJumpSound);
    }
}
