using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Interractable : MonoBehaviour
{
    public Renderer objectRenderer; // Reference to the renderer of the object
    // Material with emission property to make the object glow
    public Material glowingMaterial;

    public Transform player; // Reference to the player object
    
    private Material defaultMaterial;// Default material
    public Transform ParkHere; //reference to where the boat teleports after player gets off the boat
    public Transform SitHere; //reference to where the player sits down after interacting
    public Transform teleportPlayer; //reference to where the player sits down after interacting
    public bool satDown = false;
    public bool canPark = true;
    [SerializeField] private CinemachineFreeLook boatCam;
    [SerializeField] private CinemachineVirtualCamera newCam;
    public bool oneTimeInteraction;
    public bool hasInteracted = false;
    public bool ActivateDialogue = false;

    //public CinemachineFreeLook freeLookCamera;

    //[SerializeField] private CinemachineVirtualCamera newCam;
    [SerializeField] private CinemachineFreeLook playerCam;

    public Animator playerAnim;
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            defaultMaterial = objectRenderer.material;
        }
    }
    public void interract()
    {
        //Debug.Log("send flare now");
        //cameraTransitions.Instance.switchCameras(playerCam);
        // Check if the object has the "Puzzle" tag
        if (gameObject.CompareTag("Puzzle"))
        {
            if (oneTimeInteraction)
            {
                hasInteracted = true;
            }
            

            cameraTransitions.Instance.switchCameras(newCam, true);
            ActivateDialogue = true;
            Debug.Log("activate dialogue");

            if (objectRenderer != null && glowingMaterial != null)
            {
                // Apply glowing material to the object renderer
                objectRenderer.material = glowingMaterial;
            }

            

        }
        else if (gameObject.CompareTag("Ride"))

        {

            if (SitHere != null & satDown == false)
            {
                Debug.Log("sit on the ride");
                //get on the boat
                player.SetParent(SitHere);
                player.position = SitHere.position;
                playerAnim.SetBool("SitDown", true); // sit down animation
                
                SitHere.Rotate(0, -90, 0);
                player.GetComponent<Movement>().enabled = false;
                satDown = true;
                playerCam.Priority = 0;
                boatCam.Priority = 1;
            }
            else if (SitHere != null & satDown & canPark == true)
            {
                
                //get off the boat
                player.SetParent(null);
                
                player.position = teleportPlayer.position; //teleport the player to the shore when boat is parked
                transform.position = ParkHere.position; //teleport the boat to ther parking spot                
                transform.rotation = Quaternion.identity; // Reset the rotation of the boat 
                
                satDown = false;               
                playerCam.Priority = 10; //change back tot he player camera
                playerAnim.SetBool("SitDown", false); // sit down animation
                player.GetComponent<Movement>().enabled = true; //let the player move again 
                playerCam.Priority = 1;
                boatCam.Priority = 0;
            }
            else
            {
                Debug.LogWarning("player or sitHere reference is not set");
            }

        }
        else if (gameObject.CompareTag("Interact"))
        {

            if (objectRenderer != null && glowingMaterial != null)
            {
                // Apply glowing material to the object renderer
                objectRenderer.material = glowingMaterial;
            }
        } else
        {
            if (oneTimeInteraction)
            {
                hasInteracted = true;
            }


            cameraTransitions.Instance.switchCameras(newCam, true);

            if (objectRenderer != null && glowingMaterial != null)
            {
                // Apply glowing material to the object renderer
                objectRenderer.material = glowingMaterial;
            }
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
