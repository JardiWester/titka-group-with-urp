using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class winTriggerCode : MonoBehaviour
{

    public bool connected = false;
    [SerializeField] private pipeLogic connectedScript;
    private bool allAreConnected;
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private Vector3 currentRotation;
    

    void Update()
    {
        currentRotation = new Vector3 (connectedScript.gameObject.transform.rotation.eulerAngles.x, connectedScript.gameObject.transform.rotation.eulerAngles.y, connectedScript.gameObject.transform.rotation.eulerAngles.z);
        //if it has found a connected pipe, and the connected variable is set to true
        if (connectedScript)
        {
            if (gameObject.transform.parent.GetComponent<winGridCode>().hasToHaveCorectRotation && new Vector3 (connectedScript.gameObject.transform.rotation.eulerAngles.x, connectedScript.gameObject.transform.rotation.eulerAngles.y, connectedScript.gameObject.transform.rotation.eulerAngles.z) != targetRotation)
            {
                allAreConnected = false;
            } else if(connected == false && gameObject.GetComponentInParent<winGridCode>().hasToBeConnected)
            {
                allAreConnected = false;
            } else
            {
                //it sets allareconnected to true, just like in the pipeScript and gridManager
                allAreConnected = true;
                //it then checks all connected triggers in the script of the pipe
                foreach (GameObject trigger in connectedScript.connectionTrigers)
                {
                    //if there is no gameobject in connectedTrigger, so if it isn't connected to anything
                    if (!trigger.GetComponent<triggerCode>().connectedTrigger)
                    {
                        //it will set allAreConected to false
                        allAreConnected = false;
                        //it should stop here, without doing unnecissairy checks
                        break; 
                    }
                }
            }
            //it should set connected to whatever allAreConnected turns out to be
            connected = allAreConnected;
            //i did it this way because i thought the wingrid might check this code before finishing running this check
            //that would mess up because it is set to true at the start, true until proven otherwise, innocent by default ;)
        } else
        {
            //if the script isn't there or isn't connected, it shouldn't even try to check
            connected = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //it finds the script of the pipe this is connected to
        if (other.gameObject.GetComponent<pipeLogic>())
        {
            targetRotation = other.GetComponent<pipeLogic>().targetRotation;
            
            connected = false;
            connectedScript = other.gameObject.GetComponent<pipeLogic>();
            
        }
    }

}
