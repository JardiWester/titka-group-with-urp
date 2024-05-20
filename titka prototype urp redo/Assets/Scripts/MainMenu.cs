using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ // This is for loading the main menu screen
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit() // Quits the game
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}
