using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pipesInGrid;
    [SerializeField] private GameObject[] inlets;
    [SerializeField] private bool resetting;
    [SerializeField] private bool allHaveBeenReset;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("pipe"))
            {
                pipesInGrid.Add(child.gameObject);
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
