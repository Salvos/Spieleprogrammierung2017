using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void trigger()
    {
        animator.SetTrigger("door");
    }
}
