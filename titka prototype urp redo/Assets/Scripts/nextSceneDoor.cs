using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSceneDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //check if collided object has the players movement script
        if (other.gameObject.GetComponent<Movement>())
        {
            //if so, go to the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
