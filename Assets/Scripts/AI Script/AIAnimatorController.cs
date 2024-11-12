using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Idle()
    {
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", false);
        animator.SetBool("isSearch", false);
    }
    
    public void Search()
    {
        animator.SetBool("isSearch", true);
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", false);
    }
    public void Walk()
    {
        animator.SetBool("isWalk", true);
        animator.SetBool("isRun", false);
        animator.SetBool("isSearch", false);

    }
    public void Run()
    {
        animator.SetBool("isRun", true);
        animator.SetBool("isWalk", false);
        animator.SetBool("isSearch", false);

    }
}
