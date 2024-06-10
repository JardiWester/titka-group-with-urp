using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class breakrock : MonoBehaviour
{
    public GameObject Rocks;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movement>())
        {
            
                Rocks.SetActive(false);
                
        }


    }
}
