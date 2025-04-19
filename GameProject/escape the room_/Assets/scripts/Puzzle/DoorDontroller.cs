using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDontroller : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
    }
}
