using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraTransitions : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook playerCam;
    [SerializeField] private CinemachineVirtualCamera activeCam;
    [SerializeField] private cameraFollow camFollowScript;
    [SerializeField] private Movement playerMovement;



    public static cameraTransitions Instance { get; private set; }

    void Awake()
    {
        // Ensure that there's only one instance of this class
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Optional: Persist this instance between scenes
        // DontDestroyOnLoad(gameObject);
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            switchCameras();
        }
    }*/

    /*public void switchCameras()
    {
        if (playerCam.Priority == 0){
            playerCam.Priority = 1;
            newCam.Priority = 0;
            playerMovement.enabled = true;
            gameObject.SetActive(false);
        }else{
            playerMovement.enabled = false;
            playerCam.Priority = 0;
            newCam.Priority = 1;
            camFollowScript.coroutineAllowed = true;
        }
    }*/
    public void switchCameras(CinemachineVirtualCamera newCam, bool unlockMouse = false)
    {
        if (unlockMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (activeCam)
        {
            activeCam.Priority = 0;
            newCam.Priority = 1;
            activeCam = newCam;
        }
        else
        {
            playerMovement.enabled = false;
            playerCam.Priority = 0;
            newCam.Priority = 1;
            activeCam = newCam;
        }
    }
    public void resetCameras()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCam.Priority = 1;
        activeCam.Priority = 0;
        activeCam = null;
        playerMovement.enabled = true;
    }
}