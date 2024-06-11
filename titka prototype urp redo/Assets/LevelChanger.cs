using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Transform player;
    public PageManager pageManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player & pageManager.canEndGame)
        {
            SwitchToScene("MainMenu");
            Debug.Log("switch scenes");
        }

    }


    public void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
