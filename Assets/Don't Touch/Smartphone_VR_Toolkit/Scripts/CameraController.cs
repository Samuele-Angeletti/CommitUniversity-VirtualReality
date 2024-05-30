using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<GameObject> startInactive;
    [SerializeField] List<GameObject> startActive;
    public void IsMoving(bool moving)
    {
        animator.SetBool("Moving", moving);
    }

    public void End()
    {
        animator.SetTrigger("End");
    }

    public void RestartApp()
    {
        Application.Quit();
    }
}
