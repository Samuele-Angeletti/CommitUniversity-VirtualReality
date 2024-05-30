using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetAnimationTrigger(string action)
    {
        animator.SetTrigger(action);
    }
}
