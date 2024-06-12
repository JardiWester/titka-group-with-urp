using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSceneDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Movement>()) {
                //if so, go to the next scene
                SceneManager.LoadScene(sceneName: "FinalJungle");
        }
    }
}
