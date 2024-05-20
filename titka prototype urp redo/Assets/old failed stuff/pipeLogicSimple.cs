using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeLogicSimple : MonoBehaviour
{
    

    [SerializeField] public bool connected;
    [SerializeField] private List<GameObject> connectionTrigers;
    [SerializeField] private Material flowingMaterial;
    [SerializeField] private Material emptyMaterial;
    public int triggersDone = 0;
    private bool resetting = false;

    void Start()
    {
        emptyMaterial = gameObject.GetComponent<MeshRenderer>().material;
        //find all child objects and store them in a list
        foreach (Transform child in transform)
        {
            if (child.CompareTag("pipeTrigger"))
            {
                connectionTrigers.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (resetting && triggersDone >= connectionTrigers.Count)  
        {
            gameObject.transform.parent.GetComponent<pipeReset>().startReset = true;
        }
    }


    private void OnMouseDown()
    {
        triggersDone = 0;
        foreach (GameObject trigger in connectionTrigers)
        {
            trigger.GetComponent<trigger>().updatedConection = false;
            trigger.GetComponent<trigger>().CheckCollision();
        }   
        transform.Rotate(0f, 0f, -90f);
        resetting = true;
    }

    public void checkConected()
    {
        //set connected to true
        connected = true;
        //chek if any of the triggers are colliding with another trigger
        foreach (GameObject trigger in connectionTrigers)
        {
            trigger.GetComponent<trigger>().updatedConection = false;
            trigger.GetComponent<trigger>().checkTriggers();
        }
        //if so, check the parents connection status
        //if !connected, let it check connections
        //else do nothing 
        gameObject.GetComponent<MeshRenderer>().material = flowingMaterial;
    }

    public void resetConnection()
    {
        resetting = false;
        triggersDone = 0;
        connected = false;
        gameObject.GetComponent<MeshRenderer>().material = emptyMaterial;
        gameObject.transform.parent.GetComponent<pipeReset>().resetCount += 1;
    }



/*    public Animator animator;
    public bool connected;
    private float startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation.z;
        animator = GetComponent<Animator>();
    }


    private void OnMouseDown(){
        animator.Play("turn");
    }

    public void rotate()
    {
        Debug.Log("pipeScript");
        //transform.parent.Rotate(0f, 0f, -90f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

}
