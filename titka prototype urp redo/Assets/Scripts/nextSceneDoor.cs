using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSceneDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("JungleEnter")); {
                //if so, go to the next scene
                SceneManager.LoadScene(sceneName: "Jurrien_Jungle_Backup");
        }
    }
}
