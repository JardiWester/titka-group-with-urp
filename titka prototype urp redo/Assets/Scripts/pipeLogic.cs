using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class pipeLogic : MonoBehaviour
{
    public List<GameObject> connectionTrigers;
    [SerializeField] private List<Light> connectedLights;
    [SerializeField] private Material flowingMaterial;
    [SerializeField] private bool allHaveBeenReset;
    private Material emptyMaterial;
    public bool hasReset = false;
    public bool conected;
    public bool isResetting = false;
    private float targetRed;
    private float targetBlue;





    void Start()
    {
        transform.Rotate(0,0, Random.Range(0,4) * 90);
        
        emptyMaterial = gameObject.GetComponent<MeshRenderer>().material;
        //find all child objects and store them in a list
        foreach (Transform child in transform)
        {
            if (child.CompareTag("pipeTrigger"))
            {
                connectionTrigers.Add(child.gameObject);
            } else
            {
                if (child.GetComponent<Light>())
                {
                    connectedLights.Add(child.gameObject.GetComponent<Light>());
                    //child.GetComponent<Light>().color = Color.HSVToRGB(300, 100, 16);
                }
            }
        }
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
        foreach (Light light in connectedLights)
        {
            light.intensity = 0;
        }
    }

    void Update()
    {
        if (isResetting)
        {
            
            allHaveBeenReset = true;
            foreach (GameObject trigger in connectionTrigers)
            {
                if (trigger.GetComponent<triggerCode>().connectedTrigger == null && trigger.GetComponent<triggerCode>().hasReConected == false)
                {
                    Debug.Log("attempting a reset");
                    trigger.GetComponent<triggerCode>().attemptReconection = true;
                }
                if (trigger.GetComponent<triggerCode>().hasReConected == false)
                {
                    allHaveBeenReset = false;
                    break; // No need to continue checking if one object has the property false
                }
            }

            // If all objects have the bool property set to true, execute a certain function
            if (allHaveBeenReset)
            {
                hasReset = true;
                isResetting = false;
            }
        }
    }


    private void OnMouseDown()
    { 
        transform.Rotate(0f, 0f, -90f);
        transform.parent.GetComponent<gridManager>().resetGrid();
    }

    public void resetConnection()
    {
        foreach (GameObject trigger in connectionTrigers)
        {
            trigger.GetComponent<triggerCode>().hasReConected = false;
        }
        isResetting = true;
        conected = false;
        //gameObject.GetComponent<MeshRenderer>().material = emptyMaterial;
        
        foreach (Light light in connectedLights)
        {
            light.intensity = 0;
        }
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
        
    }
    

    public void checkConectedPipes()
    {
        //set connected to true
        conected = true;
        //chek if any of the triggers are colliding with another trigger
        foreach (GameObject trigger in connectionTrigers)
        {
            trigger.GetComponent<triggerCode>().checkConectedTriggers();
        }
        //if so, check the parents connection status
        //if !connected, let it check connections
        //else do nothing 
        //gameObject.GetComponent<MeshRenderer>().material = flowingMaterial;
        
        foreach (Light light in connectedLights)
        {
            light.intensity = 0.15f;
        }
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.1f, 0, 0.02f));
        
    }
}
