using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{ 
    public GameObject BOOK;
    public Image leftPage;
    public Image rightPage;
    public Sprite[] pageSprites; // Array to store the sprites for each page 
    public Sprite[] placeholderSprite; // Placeholder image for locked pages
    public List<bool> puzzleDone = new List<bool>();
    public bool[] puzzleCompletionStatus; // Array to track puzzle completion
    public GameObject PlayerContainer;

    public Toggle[] Toggles;
    public GameObject PuzzleList;
    [SerializeField] private GameObject mapandbookopen;
    [SerializeField] private GameObject unlocked;

    private CinemachineFreeLook playerCamera;
    
    private int currentFullPageIndex = 0; // To track the current full page being displayed
    public GameObject[] pageNo;

    [SerializeField] private CanvasGroup leftPageCanvasGroup;
    [SerializeField] private CanvasGroup rightPageCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f; // Duration for the fade-in effect

    public winGridCode winGridCode;
    public SFX SFX;
    public Interractable Interractable;
    public bool canEndGame = false;
    void Start()
    {
        canEndGame = false;

        PuzzleList.SetActive(false);
        
        // Initialize all puzzles as incomplete
        puzzleCompletionStatus = new bool[8];

        // Populate the puzzleDone list for testing purposes
        for (int i = 0; i < 8; i++)
        {
            puzzleDone.Add(false);
        }

        // Initialize comic book display with placeholders
        if (leftPageCanvasGroup == null)
        {
            leftPageCanvasGroup = leftPage.gameObject.AddComponent<CanvasGroup>();
        }
        if (rightPageCanvasGroup == null)
        {
            rightPageCanvasGroup = rightPage.gameObject.AddComponent<CanvasGroup>();
        }

        DisplayPages(currentFullPageIndex); // Display the first set of pages (1 and 2)

        

    }

    public async void GetNewPage()
    {
        await Task.Delay(1500);
        CheckPuzzleCompletion();
        unlocked.SetActive(true); //"page unlocked" prompt
    }

    private void Update()
    {
               
        ToggleBook();

        for (int i = 1; i <= 6; i++)
        {
            if (puzzleDone[i])
            {
                Debug.Log("can end game true");
                canEndGame = true;
            }
        }
       

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
        Toggles[puzzleIndex].isOn = true;
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
        

        for (int i = 0; i < pageNo.Length; i++)
        {
            //acitvates the page number based on the fullpageindex
            pageNo[i].SetActive(i == fullPageIndex);  
        }

        // Check if the left page is unlocked
        if (leftPageIndex < puzzleCompletionStatus.Length && puzzleCompletionStatus[leftPageIndex])
        {
            leftPage.sprite = pageSprites[leftPageIndex];

            if (winGridCode.fade == true)
            {
                leftPageCanvasGroup.alpha = 0;  // Reset alpha before starting fade in
                StartCoroutine(FadeInPage(leftPageCanvasGroup));
            }
            
        }
        else
        {
            leftPage.sprite = placeholderSprite[leftPageIndex];
            leftPageCanvasGroup.alpha = 1; // Ensure the placeholder is visible
        }

        // Check if the right page is unlocked
        if (rightPageIndex < puzzleCompletionStatus.Length && puzzleCompletionStatus[rightPageIndex])
        {
            rightPage.sprite = pageSprites[rightPageIndex];

            if (winGridCode.fade == true)
            {
                //rightPageCanvasGroup.alpha = 0; // Reset alpha before starting fade in
                StartCoroutine(FadeInPage(rightPageCanvasGroup));
            }
        }
        else
        {
            rightPage.sprite = placeholderSprite[rightPageIndex];
            rightPageCanvasGroup.alpha = 1; // Ensure the placeholder is visible
        }
    }

    public void NextPage()
    {
        winGridCode.fade = false;
        if (currentFullPageIndex < pageSprites.Length / 2 - 1)
        {
            SFX.PlayPageFlipSound();
            currentFullPageIndex++;
            DisplayPages(currentFullPageIndex);
        }
    }

    public void LastPage()
    {
        winGridCode.fade = false;
        if (currentFullPageIndex > 0)
        {
            SFX.PlayPageFlipSound();
            currentFullPageIndex--;
            DisplayPages(currentFullPageIndex);
        }
    }

    private void ToggleBook()
    {
        if (Input.GetKeyDown(KeyCode.E) & !winGridCode.inpuzzle)
        {
            winGridCode.fade = false;
            if (BOOK.activeSelf == false)
            { 
                //open book
                BOOK.SetActive(true);
                PuzzleList.SetActive(true);
                mapandbookopen.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
            else if (BOOK.activeSelf == true)
            {
                //close book
                BOOK.SetActive(false);
                PuzzleList.SetActive(false);
                mapandbookopen.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
               
            }
           
        }


    }
    public async void OpenBook()   //FOR OPENING THE BOOK AUTOMATICALLY
    {

            await Task.Delay(1000);
            //open book
             BOOK.SetActive(true);
            PuzzleList.SetActive(true);

            mapandbookopen.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;         
            Time.timeScale = 0f;
        
        if (BOOK.activeSelf == true & Input.GetKeyDown(KeyCode.E))
        {
            //close book
            BOOK.SetActive(false);
            PuzzleList.SetActive(false);
            unlocked.SetActive(false);
            mapandbookopen.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
           
        }
    }

    private IEnumerator FadeInPage(CanvasGroup canvasGroup) //fade effect
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            //Time.timeScale = 1f;
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
           
            yield return null;

        }

        canvasGroup.alpha = 1f;
        //Time.timeScale = 0f;
    }

}