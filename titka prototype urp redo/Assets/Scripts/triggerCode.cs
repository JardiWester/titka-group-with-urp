using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCode : MonoBehaviour
{
    public GameObject connectedTrigger;
    public bool hasReConected = false;
    public bool attemptReconection;
    


    void Update()
    {
        if (attemptReconection)
        {
            
            Collider[] colliders = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2f, transform.rotation);

            // Check if any trigger colliders are overlapping
            if (colliders.Length > 0)
            {
                foreach (Collider potentialTrigger in colliders)
                {
                    if (potentialTrigger.CompareTag("pipeTrigger"))
                    {
                        connectedTrigger = potentialTrigger.gameObject;
                    }
                }
            }
            Debug.Log("attempting reconection");
            hasReConected = true;
            attemptReconection = false;
        }
    }


    void OnTriggerExit(Collider other)
    {
        connectedTrigger = null;
        hasReConected = true;
        attemptReconection = false;
    }
    void OnTriggerEnter(Collider other)
    {
        connectedTrigger = other.gameObject;
        hasReConected = true;
        attemptReconection = false;
    }
    void OnTriggerStay(Collider other)
    {
        hasReConected = true;
        attemptReconection = false;
    }
    public void checkConectedTriggers()
    {
        if (connectedTrigger && connectedTrigger.transform.parent.GetComponent<pipeLogic>())
        {
            if (!connectedTrigger.transform.parent.GetComponent<pipeLogic>().conected)
            {
                connectedTrigger.transform.parent.GetComponent<pipeLogic>().checkConectedPipes();
            }
        }
    }
}
