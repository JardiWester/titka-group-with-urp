using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class inletLogic : MonoBehaviour
{

    [SerializeField] private bool connected;
    [SerializeField] private List<GameObject> connectionTrigers;
    [SerializeField] private GameObject connectedGrid;

    void Start()
    {
        //find all child obj and store it in a list
        foreach (Transform child in transform)
        {
            if (child.CompareTag("pipeTrigger"))
            {
                connectionTrigers.Add(child.gameObject);
            }
        }
        checkConected();
    }

    private void OnMouseDown()
    {

        if (connected){
            connected = false;
            connectedGrid.GetComponent<pipeReset>().resetPipes();
        }else {
            connected = true;
            checkConected();
        }
    }

    public void checkConected()
    {

        //chek if any of the triggers are colliding with another trigger
        foreach (GameObject trigger in connectionTrigers)
        {
            trigger.GetComponent<inletTrigger>().updatedConection = false;
            trigger.GetComponent<inletTrigger>().checkTriggers();
        }
        //if so, check the parents connection status
        //if !connected, let it check connections
        //else do nothing 
    }

    public void reConnect()
    {
        if (connected){
            checkConected();
        }
    }

}
