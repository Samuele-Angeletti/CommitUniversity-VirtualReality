using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vebano : MonoBehaviour
{
    public Animator animator;
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
