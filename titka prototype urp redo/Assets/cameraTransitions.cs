using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraTransitions : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook playerCam;
    [SerializeField] private CinemachineVirtualCamera activeCam;
    [SerializeField] private cameraFollow camFollowScript;

    [SerializeField] private GameObject PlayerContainer;
    private Movement playerMovement;
    private CinemachineFreeLook playerCamera;
    [SerializeField]private float CamSpeedX = 0.1f;
    [SerializeField]private float CamSpeedY = 6f;
    


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

    void Start()
    {
        playerCamera = PlayerContainer.GetComponentInChildren<CinemachineFreeLook>();
        playerMovement = PlayerContainer.GetComponentInChildren<Movement>();
        Debug.Log("it does indeed start");
        playerCamera.m_YAxis.m_MaxSpeed = CamSpeedY;
        playerCamera.m_XAxis.m_MaxSpeed = CamSpeedX;

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
        Debug.Log("bruuuuuuuuuuuh");
        if (unlockMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerCamera.m_YAxis.m_MaxSpeed = 0;
            playerCamera.m_XAxis.m_MaxSpeed = 0;
            playerMovement.enabled = false;
        }else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        playerCamera.m_YAxis.m_MaxSpeed = 0;
        playerCamera.m_XAxis.m_MaxSpeed = 0;
        playerMovement.enabled = false;
        Debug.Log("camTrigger");
        if (activeCam)
        {
            activeCam.Priority = 0;
            newCam.Priority = 1;
            activeCam = newCam;
        }else 
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
        playerCamera.m_YAxis.m_MaxSpeed = CamSpeedY;
        playerCamera.m_XAxis.m_MaxSpeed = CamSpeedX;
        playerMovement.enabled = true;
        playerCam.Priority = 1;
        if (activeCam){
            activeCam.Priority = 0;
        }
        activeCam = null;
        playerMovement.enabled = true;
    }
}
