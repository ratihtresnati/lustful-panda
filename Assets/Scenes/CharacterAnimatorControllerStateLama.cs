using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorControllerStateLama : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void WalkSpeed(float horizontal, float vertical)
    {
        _animator.SetFloat("horizontal", horizontal);
        _animator.SetFloat("vertical", vertical);
    }

    public void Turn(float turns)
    {
        _animator.SetFloat("Turn", turns);
    }

    public void Idle()
    {
        _animator.SetBool("isIdle", true);
        _animator.SetBool("isWalk", false);
        _animator.SetBool("isRun", false);
        _animator.SetBool("isJump", false);
        _animator.SetBool("isRolling", false);
        _animator.SetBool("isSneak", false);
        _animator.SetBool("isRest", false);
        _animator.SetBool("isSit", false);
    }
    public void Walk()
    {
        _animator.SetBool("isWalk", true);
        _animator.SetBool("isRun", false);
        _animator.SetBool("isIdle", false);
    }

    public void Running()
    {
        _animator.SetBool("isRun", true);
        _animator.SetBool("isWalk", false);
        _animator.SetBool("isIdle", false);
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
        _animator.SetBool("isAction", true);
        _animator.SetBool("isRest", true);
    }

    public void UpRest()
    {
        _animator.SetBool("isRest", false);
        _animator.SetBool("isAction", false);
    }

    public void Sit()
    {
        _animator.SetBool("isAction", true);
        _animator.SetBool("isSit", true);
    }

    public void UpSit()
    {
        _animator.SetBool("isSit", false);
        _animator.SetBool("isAction", false);
    }

    public IEnumerator Catching()
    {
        yield return new WaitForSeconds(0.8f);

        _animator.SetBool("isCatch", true);
    }

    public void Left()
    {
        _animator.SetBool("turnLeft", true);
        _animator.SetBool("turnRight", false);
    }
    public void Right()
    {
        _animator.SetBool("turnRight", true);
        _animator.SetBool("turnLeft", false);
    }

    public void Noturn()
    {
        _animator.SetBool("turnRight", false);
        _animator.SetBool("turnLeft", false);
    }
}
