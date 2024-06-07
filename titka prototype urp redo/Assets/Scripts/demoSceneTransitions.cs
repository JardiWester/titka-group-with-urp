using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class demoSceneTransitions : MonoBehaviour
{
    private float timer;
    [SerializeField] float duration; //the hold delay
    
    void Update()
    {
        //when you start pressing =
        if(Input.GetKeyDown(KeyCode.Equals))
        {
            //it sets the timer to the current game time
            timer = Time.time;
        }
        else if(Input.GetKey(KeyCode.Equals))//and while you are holding it:
        {
            //it will check if the difference between the current game time and
            //the buffered time is more than the hold duration
            if(Time.time - timer > duration)
            {
                //if so, iut goes to the next scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        //does the same checks for -
        if(Input.GetKeyDown(KeyCode.Minus))
        {
            timer = Time.time;
        }
        else if(Input.GetKey(KeyCode.Minus))
        {
            if(Time.time - timer > duration)
            {
                //but goes a scene back instead
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        //does the same for the non numpad 0
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            timer = Time.time;
        }
        else if(Input.GetKey(KeyCode.Alpha0))
        {
            if(Time.time - timer > duration)
            {
                //but goes back to the first scene
                SceneManager.LoadScene(0);
            }
        }
    }
}
