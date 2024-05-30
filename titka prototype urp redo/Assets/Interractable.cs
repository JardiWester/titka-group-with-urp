using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Interractable : MonoBehaviour
{
    public Renderer objectRenderer; // Reference to the renderer of the object
    // Material with emission property to make the object glow
    public Material glowingMaterial;

    // Default material
    private Material defaultMaterial;

    [SerializeField] private CinemachineVirtualCamera newCam;
    public bool oneTimeInteraction;
    public bool hasInteracted = false;

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
        if (oneTimeInteraction){
            hasInteracted = true;
        }
        Debug.Log("send flare now");

        cameraTransitions.Instance.switchCameras(newCam, true);

        if (objectRenderer != null && glowingMaterial != null)
        {
            // Apply glowing material to the object renderer
            objectRenderer.material = glowingMaterial;
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
