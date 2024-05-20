using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turning : StateMachineBehaviour
{
    private Animator animator;
    public bool boolVariable;
    private pipeLogic pipeScript;



    /*
    // This method is called by the animation event
    public void CheckBool()
    {
        if (!boolVariable)
        {
            animator.Play("empty");
        }
        else
        {
            animator.Play("full");
        }
    }
    */

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Debug.Log("teettesdt");
            pipeScript = animator.gameObject.GetComponent<pipeLogic>();
            //animator.gameObject.transform.parent.Rotate(0f, 0f, -90f);
            Debug.Log(pipeScript);
            
            /*
            pipeScript.rotate();
            if (!pipeScript.connected)
            {
                animator.Play("empty");
            }else
            {
                animator.Play("full");
            }*/
        }
    }
   
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    /*override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("teettesdt");
        pipeScript = animator.gameObject.GetComponent<pipeLogic>();
        animator.gameObject.transform.parent.Rotate(0f, 0f, -90f);
        Debug.Log(pipeScript);
        pipeScript.rotate();
        if (!pipeScript.connected)
        {
            animator.Play("empty");
        }else
        {
            animator.Play("full");
        }
    }*/
}
