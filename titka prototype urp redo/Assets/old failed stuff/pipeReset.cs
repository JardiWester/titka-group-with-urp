using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeReset : MonoBehaviour
{
    [SerializeField] private List<GameObject> pipesInGrid;
    [SerializeField] private GameObject[] inlets;
    [SerializeField] private bool reset;
    public bool startReset;
    public int resetCount;


    // Start is called before the first frame update
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

    void Update()  
    {
        if (startReset)
        {
            startReset = false;
            resetPipes();
        }
        if (reset)
        {
            if (resetCount >= pipesInGrid.Count)
            {
                Debug.Log("reconnecting");
                foreach (GameObject inlet in inlets)
                {
                    inlet.GetComponent<inletLogic>().reConnect();
                }
                reset = false;
            }
        }
    }

    public void resetPipes()
    {
        Debug.Log("reseting pipes");
        resetCount = 0;
        foreach (GameObject pipe in pipesInGrid)
        {
            pipe.GetComponent<pipeLogicSimple>().resetConnection();
        }
        reset = true;
        Debug.Log("amount of objects in pipesInGrid" + pipesInGrid.Count);
    }
}
