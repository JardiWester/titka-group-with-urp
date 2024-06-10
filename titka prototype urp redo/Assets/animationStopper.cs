using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStopper : MonoBehaviour
{
    public Animator animator;
    private Movement playerScript;


    private void Start()
    {
        playerScript = GetComponent<Movement>();
    }

    private void Update()
    {
        if (playerScript.enabled == false)
        {
            animator.SetBool("jumpAnim", false);
            animator.SetBool("AnimWalk", false);
            animator.SetBool("runJumpAnim", false);
            animator.SetBool("RunAnim", false);
        }
    }



}
