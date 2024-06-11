using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    public Animator animate;
    //public Animator monkeanim;
    [SerializeField] private bool phone = false;
    [SerializeField] private bool idle = false;
    [SerializeField] private bool idle2 = false;
    [SerializeField] private bool talk = false;
    [SerializeField] private bool sit = false;
    [SerializeField] private bool sit2 = false;
    [SerializeField] private bool sillysit = false;
    [SerializeField] private bool swimfloat = false;
    [SerializeField] private bool swim = false;
    [SerializeField] private bool breakdance = false;
    [SerializeField] private bool monkewalk = false;
    [SerializeField] private bool walk = false;
    [SerializeField] private bool monkeswing = false;

    private void Update()
    {
        if (phone)
        {
            
            animate.SetBool("phone", true);
            animate.enabled = true;
        }
        else if (idle)
        {
            
            animate.SetBool("idle", true);
            animate.enabled = true;
        }
        else if (talk)
        {
            
            animate.SetBool("talk", true);
            animate.enabled = true;
        }
        else if (sit)
        {
            
            animate.SetBool("sit", true);
            animate.enabled = true;
        }
        else if (idle2)
        {
            
            animate.SetBool("idle2", true);
            animate.enabled = true;
        }
        else if (sit2)
        {
            
            animate.SetBool("sit2", true);
            animate.enabled = true;
        }
        else if (sillysit)
        {
            
            animate.SetBool("sillysit", true);
            animate.enabled = true;
        }
        else if (swim)
        {
            
            animate.SetBool("swim", true);
            animate.enabled = true;
        }else if (swimfloat)
        {
            animate.SetBool("swimfloat", true);
            animate.enabled = true;
        }
        else if (breakdance)
        {
            
            animate.SetBool("breakdance", true);
            animate.enabled = true;
        }
        else if (monkewalk)
        {
            
            animate.SetBool("monkewalk", true);
            animate.enabled = true;
        }
        else if (walk)
        {
            
            animate.SetBool("walk", true);
            animate.enabled = true;
        }
        else if (monkeswing)
        {
            animate.SetBool("monkeswing", true);
            animate.enabled = true;
        }
        else
        {
            animate.enabled = false;
        }
    }





}
