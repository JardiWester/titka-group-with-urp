using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Index
{
    public int value;
}

public class winGridCode : MonoBehaviour
{
    private bool winCheck = false;
    public List<GameObject> winTriggersInGrid = new List<GameObject>();
    [SerializeField] private bool allAreConnected;
    [SerializeField] private GameObject winIndicator;
    public bool hasToBeConnected = true;
    public bool hasToHaveCorectRotation = false;
    [SerializeField] private bool InBoat;
    [SerializeField] private PageManager pageManager;

    [SerializeField] public Index WinPageNumber;
    public bool inpuzzle;
    public bool fade;
    public GameObject puzzletutorial;

    void Start()
    {
        inpuzzle = false;

        // Initialize winTriggersInGrid
        foreach (Transform child in transform)
        {
            winTriggersInGrid.Add(child.gameObject);
        }

        // Ensure WinPageNumber is initialized
        if (WinPageNumber == null)
        {
            WinPageNumber = new Index();
        }

        // Ensure pageManager is assigned (you can assign it in the Inspector or find it by code)
        if (pageManager == null)
        {
            pageManager = FindObjectOfType<PageManager>();
            if (pageManager == null)
            {
                Debug.LogError("PageManager not found in the scene.");
            }
        }

        // Ensure puzzleCompletionStatus is initialized
        if (pageManager.puzzleDone == null)
        {
            //pageManager.puzzleDone = new Dictionary<int, bool>();
        }
    }

    void Update()
    {
        // Check all winTriggers
        allAreConnected = true;
        foreach (GameObject winTrigger in winTriggersInGrid)
        {
            if (!winTrigger.GetComponent<winTriggerCode>().connected)
            {
                allAreConnected = false;
                break;
            }
        }

        if (allAreConnected && !winCheck)
        {
            winCheck = true;
            Debug.Log("YOU WIN!!!!!!!!!!!!!!!!!!!!!");
            inpuzzle = false;
            puzzletutorial.SetActive(false); 
            cameraTransitions.Instance.resetCameras(InBoat);

            // Ensure WinPageNumber has a valid value
            if (WinPageNumber != null)
            {
                fade = true;
                pageManager.OpenBook();
                pageManager.puzzleDone[WinPageNumber.value] = true;                
                pageManager.GetNewPage();
                
            }
            else
            {
                Debug.LogError("WinPageNumber is null.");
            }
        }

        if (gameObject.transform.parent.GetComponent<Interractable>().hasInteracted && !winCheck)
        {
            inpuzzle = true;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                inpuzzle = false;
                puzzletutorial.SetActive(false);
                cameraTransitions.Instance.resetCameras(InBoat);

                // Ensure WinPageNumber has a valid value
                if (WinPageNumber != null)
                {
                    pageManager.puzzleDone[WinPageNumber.value] = true;
                    pageManager.GetNewPage();
                }
                else
                {
                    Debug.LogError("WinPageNumber is null.");
                }
                gameObject.transform.parent.GetComponent<Interractable>().hasInteracted = false;
            }
        }



        
    }
}