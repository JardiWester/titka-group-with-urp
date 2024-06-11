using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public winGridCode winGrid;
    public static bool isPaused;
    public GameObject controls;
    
    // Start is called before the first frame update
    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) 
            { 
                ResumeGame(); 
            }
            else 
            {
                PauseGame();
            }
        }
    }
    public void PauseGame ()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void ResumeGame () 
    {
        controls.SetActive(false);
        pauseMenu.SetActive(false );
        Time.timeScale = 1f;
        if (winGrid.inpuzzle == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        

        isPaused = false;   
    }


    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("quit game");
    }


    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }


    public void opencontrols()
    {
       controls.SetActive(true);
       pauseMenu.SetActive(false);
       Time.timeScale = 0f;
       Cursor.visible = true;
       Cursor.lockState = CursorLockMode.None;
    }

}
