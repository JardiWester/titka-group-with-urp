using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    public Animator animate;
    [SerializeField] private bool phone = false;
    [SerializeField]  private bool idle = false;
    [SerializeField] private bool idle2 = false;
    [SerializeField] private bool talk = false;
    [SerializeField] private bool sit = false;



    private void Start()
    {
        if (phone)
        {
            animate.SetBool("phone", true);
        }else if (idle)
        {
            animate.SetBool("idle", true);
        }else if (talk)
        {
            animate.SetBool("talk", true);
        }else if (sit)
        {
            animate.SetBool("sit", true);
        }else if (idle2)
        {
            animate.SetBool("idle2", true);
        }



    }
    




    

}
