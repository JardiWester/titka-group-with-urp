using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inletCode : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectionTrigers;
    [SerializeField] private bool isTurnedOn;
    [SerializeField] private GameObject connectedGrid;


    private void OnMouseDown()
    { 
        isTurnedOn = !isTurnedOn;
        connectedGrid.GetComponent<gridManager>().resetGrid();
    }

    void Start()
    {
        //find all child objects and store them in a list
        foreach (Transform child in transform)
        {
            if (child.CompareTag("pipeTrigger"))
            {
                connectionTrigers.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void recalculate()
    {
        Debug.Log("recalculating");
        if (isTurnedOn)
        {
            foreach (GameObject trigger in connectionTrigers)
            {
                trigger.GetComponent<triggerCode>().checkConectedTriggers();
            }
        }
    }
}
