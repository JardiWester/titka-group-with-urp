using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class Interractable : MonoBehaviour
{
    //public Renderer objectRenderer; // Reference to the renderer of the object
    // Material with emission property to make the object glow
    //public Material glowingMaterial;

    public Transform player; // Reference to the player object

    //private Material defaultMaterial;// Default material
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
    public InputUIManager InputUIManager;
    public GameObject tutorial;
    public BoatPark BoatPark;
    
    //public CinemachineFreeLook freeLookCamera;

    //[SerializeField] private CinemachineVirtualCamera newCam;
    [SerializeField] private CinemachineFreeLook playerCam;

    [SerializeField] private GameObject boatCube;

    public Animator playerAnim;
    private void Start()
    {
        
        /*objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            defaultMaterial = objectRenderer.material;
        }*/
    }
    public void interract()
    {
        //Debug.Log("send flare now");
        //cameraTransitions.Instance.switchCameras(playerCam);
        // Check if the object has the "Puzzle" tag
        if (gameObject.CompareTag("Puzzle"))
        {
            tutorial.SetActive(true);
            if (oneTimeInteraction)
            {
                hasInteracted = true;
            }

            cameraTransitions.Instance.switchCameras(newCam, true);
            ActivateDialogue = true;
            

           /* if (objectRenderer != null && glowingMaterial != null)
            {
                // Apply glowing material to the object renderer
                objectRenderer.material = glowingMaterial;
            }*/


        }
        else if (gameObject.CompareTag("Ride"))

        {
            //InputUIManager.hideExclamation();

            if (SitHere != null & satDown == false)
            {
                tutorial.SetActive(true);
                //get on the boat
                player.SetParent(SitHere);
                player.position = SitHere.position;
                playerAnim.SetBool("SitDown", true); // sit down animation

                SitHere.Rotate(0, -90, 0);
                player.GetComponent<Movement>().enabled = false;
                satDown = true;
                playerCam.Priority = 0;
                boatCam.Priority = 1;
                boatCube.GetComponent<boatFollow>().moveAllowed = true;
                boatCube.GetComponent<boatFollow>().coroutineAllowed = true;
            }
            else if (SitHere != null & satDown & canPark == true)
            {

                //get off the boat
                player.SetParent(null);

                boatCube.GetComponent<boatFollow>().moveAllowed = false;

                player.position = BoatPark.teleportPlayer.position; //teleport the player to the shore when boat is parked
                boatCube.transform.position = BoatPark.ParkHere.position; //teleport the boat to ther parking spot                
                boatCube.transform.rotation = Quaternion.identity; // Reset the rotation of the boat 

                satDown = false;
                playerCam.Priority = 1; //change back tot he player camera
                playerAnim.SetBool("SitDown", false); // sit down animation
                player.GetComponent<Movement>().enabled = true; //let the player move again 
                
                boatCam.Priority = 0;
            }
            else
            {
                //Debug.LogWarning("player or sitHere reference is not set");
            }
           
        }
        else if (gameObject.CompareTag("Interact"))
        {
            //InputUIManager.hideExclamation();

            /*if (objectRenderer != null && glowingMaterial != null)
            {
                // Apply glowing material to the object renderer
                objectRenderer.material = glowingMaterial;
            }*/
        }else
        {
            if (oneTimeInteraction)
            {
                hasInteracted = true;
            }


            cameraTransitions.Instance.switchCameras(newCam, true);

           /* if (objectRenderer != null && glowingMaterial != null)
            {
                // Apply glowing material to the object renderer
                objectRenderer.material = glowingMaterial;
            }*/
        }
    }

    public void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /*public void glow()
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
    }*/


}
