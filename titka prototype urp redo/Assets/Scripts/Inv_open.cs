using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;




public class Inv_open : MonoBehaviour


{
    public List<GameObject> pages = new List<GameObject>(); // List to store references to all pages
    private int currentPageIndex = 0; // Index of the current active page
    //public InterractController interractController;
    
    private void Start()
    {
        // Deactivate all pages when the game starts
        foreach (GameObject page in pages)
        {
            page.gameObject.SetActive(false);
           
        }
    }

    private void Update()
    {
        // Toggle page when E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePage();
            
        }
    }

    public void NextPage()
    {
        // Deactivate the current page
        pages[currentPageIndex].gameObject.SetActive(false);

        // Increment the page index
        currentPageIndex++;

        // Wrap around if we've reached the end of the list
        if (currentPageIndex >= pages.Count)
        {
            currentPageIndex = 0;
        }

        // Activate the new current page
        pages[currentPageIndex].gameObject.SetActive(true);
    }

    public void LastPage()
    {
        // Deactivate the current page
        pages[currentPageIndex].gameObject.SetActive(false);

        // Increment the page index
        currentPageIndex--;

        // Wrap around if we've reached the start of the list
        if (currentPageIndex < 0)
        {
            currentPageIndex = 2;
        }

        // Activate the new current page
        pages[currentPageIndex].gameObject.SetActive(true);
    }

    public void TogglePage()
    {
        if (pages[currentPageIndex].gameObject.activeSelf)
        {
            //close the book
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            pages[currentPageIndex].gameObject.SetActive(false);
        } else
        {
            //open the book
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pages[currentPageIndex].gameObject.SetActive(true);

        }


    }

    

}
