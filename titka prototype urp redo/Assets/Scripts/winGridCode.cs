using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winGridCode : MonoBehaviour
{
    private bool winCheck = false;
    [SerializeField] private List<GameObject> winTriggersInGrid;
    [SerializeField] private bool allAreConnected;
    [SerializeField] private GameObject winIndicator;

    void Start()
    {
        //put all the wintriggers in a list
        foreach (Transform child in transform)
        {
            winTriggersInGrid.Add(child.gameObject);
        }
    }

    void Update()
    {
        //the same as all others, it first set this to true
        allAreConnected = true;
        foreach (GameObject winTrigger in winTriggersInGrid)
        {
            //it then checks all connected wintriggers
            if (winTrigger.GetComponent<winTriggerCode>().connected == false)
            {
                //if any of them arent connected, it sets it to false
                allAreConnected = false;
                break; 
                //stop checking, it is futile, you've already lost, it's too late
            }
        }

        if (allAreConnected == true && winCheck == false)
        {   
            winCheck = true;
            Debug.Log("YOU WIN!!!!!!!!!!!!!!!!!!!!!");
        } 

    }

}
