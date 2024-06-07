using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class gridManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pipesInGrid;
    [SerializeField] private GameObject[] inlets;
    [SerializeField] private bool resetting;
    [SerializeField] private bool allHaveBeenReset;
    [SerializeField] private GameObject wingrid;
    [SerializeField] private GameObject winTriggerPrefab;

    void Start()
{
    foreach (Transform child in transform)
    {
        if (child.CompareTag("pipe"))
        {
            pipesInGrid.Add(child.gameObject);
            
            // Instantiate the winTriggerPrefab
            GameObject newWintrigger = Instantiate(winTriggerPrefab);
            
            // Set the parent of the newWintrigger to the win grid object
            newWintrigger.transform.SetParent(wingrid.transform);
            
            // Add the newWintrigger to the winTriggersInGrid list
            wingrid.GetComponent<winGridCode>().winTriggersInGrid.Add(newWintrigger);
            
            // Set the position of the newWintrigger to match the child position
            newWintrigger.transform.rotation = child.transform.rotation;
            newWintrigger.transform.localScale = new Vector3 (child.transform.localScale.x, child.transform.localScale.y, child.transform.localScale.z / 2);
            newWintrigger.transform.position = child.transform.position;
        }
    }
}

    // Update is called once per frame
    void Update()
    {
        if (resetting){
            allHaveBeenReset = true;
            foreach (GameObject pipe in pipesInGrid)
            {
                if (pipe.GetComponent<pipeLogic>().hasReset == false)
                {
                    allHaveBeenReset = false;
                    break; // No need to continue checking if one object has the property false
                }
            }

            // If all objects have the bool property set to true, execute a certain function
            if (allHaveBeenReset)
            {
                foreach (GameObject pipe in pipesInGrid)
                {
                    pipe.GetComponent<pipeLogic>().hasReset = false;
                }
                resetting = false;
                recalculateGrid();
            }
        }

    }


    public void resetGrid()
    {
        resetting = true;
        foreach (GameObject pipe in pipesInGrid)
        {
            pipe.GetComponent<pipeLogic>().resetConnection();
        }
    }

    public void recalculateGrid()
    {
        foreach(GameObject inlet in inlets)
        {
            inlet.GetComponent<inletCode>().recalculate();
        }
    }
}
