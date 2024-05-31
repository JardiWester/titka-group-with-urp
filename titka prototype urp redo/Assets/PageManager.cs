using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public GameObject BookBase;
    public Image leftPage;
    public Image rightPage;
    public Sprite[] pageSprites; // Array to store the sprites for each page
    public Sprite placeholderSprite; // Placeholder image for locked pages
    public List<bool> puzzleDone = new List<bool>();
    private bool[] puzzleCompletionStatus; // Array to track puzzle completion
    public GameObject PlayerContainer;
    //private Movement PlayerMovementScript;
    private CinemachineFreeLook playerCamera;
    //private float CamSpeedX;
    //private float CamSpeedY;
    private int currentFullPageIndex = 0; // To track the current full page being displayed

    void Start()
    {
        // Initialize all puzzles as incomplete
        puzzleCompletionStatus = new bool[6];

        // Populate the puzzleDone list for testing purposes
        for (int i = 0; i < 6; i++)
        {
            puzzleDone.Add(false);
        }

        // Initialize comic book display with placeholders
        DisplayPages(currentFullPageIndex); // Display the first set of pages (1 and 2)

        /*playerCamera = PlayerContainer.GetComponentInChildren<CinemachineFreeLook>();
        PlayerMovementScript = PlayerContainer.GetComponentInChildren<Movement>();

        CamSpeedY = playerCamera.m_YAxis.m_MaxSpeed;
        CamSpeedX = playerCamera.m_XAxis.m_MaxSpeed;*/
            
    }

    public void GetNewPage()
    {
        
    CheckPuzzleCompletion();
               
    }

    private void Update()
    {
        ToggleBook();
    }
    public void CompletePuzzle(int puzzleIndex)
    {
        if (puzzleIndex < 0 || puzzleIndex >= puzzleCompletionStatus.Length)
        {
            Debug.LogError("Invalid puzzle index");
            return;
        }

        puzzleCompletionStatus[puzzleIndex] = true;
        UpdateComicBook(puzzleIndex);
    }

    public void CheckPuzzleCompletion()
    {
        for (int i = 0; i < puzzleDone.Count; i++)
        {
            if (puzzleDone[i])
            {
                CompletePuzzle(i);
            }
        }
    }

    private void UpdateComicBook(int puzzleIndex)
    {
        int fullPageIndex = puzzleIndex / 2;
        DisplayPages(fullPageIndex);
    }

    private void DisplayPages(int fullPageIndex)
    {
        int leftPageIndex = fullPageIndex * 2;
        int rightPageIndex = leftPageIndex + 1;

        // Check if the left page is unlocked
        if (leftPageIndex < puzzleCompletionStatus.Length && puzzleCompletionStatus[leftPageIndex])
        {
            leftPage.sprite = pageSprites[leftPageIndex];
            Debug.Log("L: Loading page " + leftPageIndex);
        }
        else
        {
            leftPage.sprite = placeholderSprite;
            
        }

        // Check if the right page is unlocked
        if (rightPageIndex < puzzleCompletionStatus.Length && puzzleCompletionStatus[rightPageIndex])
        {
            rightPage.sprite = pageSprites[rightPageIndex];
            Debug.Log("R: Loading page " + rightPageIndex);
        }
        else
        {
            rightPage.sprite = placeholderSprite;
            
        }
    }

    public void NextPage()
    {
        if (currentFullPageIndex < pageSprites.Length / 2 - 1)
        {
            currentFullPageIndex++;
            DisplayPages(currentFullPageIndex);
        }
    }

    public void LastPage()
    {
        if (currentFullPageIndex > 0)
        {
            currentFullPageIndex--;
            DisplayPages(currentFullPageIndex);
        }
    }

    private void ToggleBook()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (BookBase.activeSelf == false)
            {
                //open book
                BookBase.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;


                /*playerCamera.m_YAxis.m_MaxSpeed = 0;
                playerCamera.m_XAxis.m_MaxSpeed = 0;
                PlayerMovementScript.enabled = false;*/
                Time.timeScale = 0f;
            }
            else if (BookBase.activeSelf == true)
            {
                //close book
                BookBase.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                /* playerCamera.m_YAxis.m_MaxSpeed = CamSpeedY;
                 playerCamera.m_XAxis.m_MaxSpeed = CamSpeedX;
                 PlayerMovementScript.enabled = true;*/
            }
           
        }
    }
}