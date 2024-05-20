using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUIManager : MonoBehaviour
  
{
    InterractController InterractControllerr;
    [SerializeField] private GameObject KeybindIndicator; // Reference to the UI Image GameObject (your logo)
    private bool isLoaded = false; // Flag to track whether the logo is currently loaded

    private void Start()
    {
        KeybindIndicator.SetActive(false);
    }

    // Function to show the logo
    public void ShowLogo(Vector3 position)
    {
        // Make sure the logo prefab is assigned and it's not already loaded
        if (KeybindIndicator != null && !isLoaded)
        {
            // Position the logo at the specified position
            gameObject.transform.position = position ;

            

            // Activate the logo
            KeybindIndicator.SetActive(true);
            gameObject.transform.LookAt(Camera.main.transform);

            Debug.Log("Image should be loaded");
            isLoaded = true;
            
        }
    }

    // Function to hide the logo
    public void HideLogo()
    {


        // Check if the logo is loaded and active
        if (isLoaded && KeybindIndicator.activeSelf)
        {

            // Deactivate the logo
            KeybindIndicator.SetActive(false);

            
            isLoaded = false; // Reset the flag
            
        }
    }

    void Update()
    {

        // Make the logo always face the player camera
        gameObject.transform.LookAt(Camera.main.transform);
    }

}

