using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Interractable : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Renderer objectRenderer; // Reference to the renderer of the object
    // Material with emission property to make the object glow
    public Material glowingMaterial;
    
    // Default material
    private Material defaultMaterial;
    public Transform SitHere; //reference to where the player sits down after interacting
    public bool satDown = false;
    public CinemachineFreeLook freeLookCamera;
    
    private void Start()
    {
        if (objectRenderer != null)
        {
            defaultMaterial = objectRenderer.material;
        }
    }
    public void interract()
    {
        // Check if the object has the "Puzzle" tag
        if (gameObject.CompareTag("Puzzle"))
        {
            Debug.Log("Object has the Puzzle tag");

        }
        else if (gameObject.CompareTag("Ride"))
        {
            if (SitHere != null & satDown == false)
            {
                player.position = SitHere.position;
                player.SetParent(SitHere);
                player.GetComponent<Movement>().enabled = false;
                satDown = true;
                freeLookCamera.Priority = 8;
            }
            else if (SitHere != null & satDown == true)
            {
                //get off the boat
                player.SetParent(null);
                satDown = false;
                freeLookCamera.Priority = 10;
                player.GetComponent<Movement>().enabled = true;
            }
            else
            {
                Debug.LogWarning("player or sitHere reference is not set");
            }

        }
        else if (gameObject.CompareTag("Interact"))
        {
            Debug.Log("Object has the Interact tag");
            
        }

    }

    public void glow()
    {
       
        if (objectRenderer != null && glowingMaterial != null)
        {
            // Apply glowing material to the object renderer
            objectRenderer.material = glowingMaterial;
        }

    }

    
    public void ResetMaterial()
    {
        if (objectRenderer != null && defaultMaterial != null)
        {
            // Reset material to default
            objectRenderer.material = defaultMaterial;
        }
    }


}
