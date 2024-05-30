using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraPanTrigger : MonoBehaviour
{

    public Movement playerMovement;
    public CinemachineVirtualCamera newCam;
    public cameraFollow camFollowScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            cameraTransitions.Instance.switchCameras(newCam);
            playerMovement.enabled = false;
            camFollowScript.coroutineAllowed = true;
        }
    }
}
