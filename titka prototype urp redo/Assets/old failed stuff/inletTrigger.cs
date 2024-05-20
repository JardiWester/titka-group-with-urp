using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inletTrigger : MonoBehaviour
{
    [SerializeField] private GameObject connectedTrigger;
    public bool updatedConection = true;




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
    void OnTriggerEnter(Collider other)
    {
        connectedTrigger = other.gameObject;
        updatedConection = true;
    }
    void OnTriggerExit(Collider other){
        connectedTrigger = null;
        updatedConection = true;
    }
    void OnTriggerStay(Collider other)
    {
        if (!updatedConection)
        {
            updatedConection = true;
        }
    }
}
