using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraTransitions : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook playerCam;
    [SerializeField] private CinemachineVirtualCamera newCam;
    [SerializeField] private cameraFollow camFollowScript;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            switchCameras();
        }
    }

    public void switchCameras()
    {
        if (playerCam.Priority == 0){
            playerCam.Priority = 1;
            newCam.Priority = 0;
            gameObject.SetActive(false);
        }else{
            playerCam.Priority = 0;
            newCam.Priority = 1;
            camFollowScript.coroutineAllowed = true;
        }
    }
}
