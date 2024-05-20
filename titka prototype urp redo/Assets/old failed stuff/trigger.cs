using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class trigger : MonoBehaviour
{
    [SerializeField] private GameObject connectedTrigger;
    private pipeLogicSimple parentScript = null;
    public bool updatedConection = true;
    private BoxCollider boxCollider;
    [SerializeField] private string testMarkingDone;

    void Start()
    {
        parentScript = gameObject.transform.parent.GetComponent<pipeLogicSimple>();
    }




    public void checkTriggers()
    {
        //check all collided objects
        //see if they are also a trigger
        //if so, check if the parents connected var is set to false
        //if so, execute checkConnected on its parent
        if (connectedTrigger && connectedTrigger.transform.parent.GetComponent<pipeLogicSimple>())
        {
            if (!connectedTrigger.transform.parent.GetComponent<pipeLogicSimple>().connected)
            {
                connectedTrigger.transform.parent.GetComponent<pipeLogicSimple>().checkConected();
            }
        } else  
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
        }

    }

    public void CheckCollision()
    {
        // Get all colliders currently overlapping with the trigger collider
        Collider[] colliders = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2f, transform.rotation);

        // Check if any colliders are overlapping
        if (colliders.Length == 0 && updatedConection == false)
        {
            parentScript.triggersDone += 1;
            updatedConection = true;
            connectedTrigger = null;
        }
    }

    void Update()   
    {
        if (updatedConection == false)
        {
            CheckCollision();
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pipeTrigger"))
        {
            // Add the collided object to the connectedObject variable
            connectedTrigger = collision.gameObject;
            Debug.Log("Connected to: " + collision.gameObject);
        }else{
            Debug.Log("not connected to: " + collision.gameObject);
        }
    }*/
    void OnTriggerExit(Collider other){

        connectedTrigger = null;
        if (updatedConection == false){
            parentScript.triggersDone += 1;
            updatedConection = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        connectedTrigger = other.gameObject;
        if (updatedConection == false)
        {
            parentScript.triggersDone += 1;
            updatedConection = true;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (updatedConection == false)
        {
            updatedConection = true;
            parentScript.triggersDone += 1;
            Debug.Log("marking done" + testMarkingDone);
        }
    }
}
