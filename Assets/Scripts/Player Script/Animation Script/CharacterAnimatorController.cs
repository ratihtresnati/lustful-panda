using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void WalkSpeed(float horizontal,float vertical)
    {
        _animator.SetFloat("horizontal", horizontal);
        _animator.SetFloat("vertical", vertical);
    }

    public void Jump()
    {
        _animator.SetBool("isJump", true);
    }

    public void Land()
    {
        _animator.SetBool("isJump", false);
    }

    public void Roll()
    {
        _animator.SetBool("isRolling", true);
    }

    public void StopRoll()
    {
        _animator.SetBool("isRolling", false);
    }

    public void Sneak()
    {
        _animator.SetBool("isSneak", true);
    }

    public void Rest()
    {
        _animator.SetBool("Action", true);
        _animator.SetBool("isRest", true);
    }

    public void UpRest()
    {
        _animator.SetBool("isRest", false);
        _animator.SetBool("Action", false);
    }

    public void Sit()
    {
        _animator.SetBool("Action", true);
        _animator.SetBool("isSit", true);
    }

    public void UpSit()
    {
        _animator.SetBool("isSit", false);
        _animator.SetBool("Action", false);
    }
}
