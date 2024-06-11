using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    // Parameters for the back-and-forth effect on the Z-axis
    public float amplitudeZ = 0.5f; // How far the object will move back and forth on the Z-axis
    public float frequencyZ = 1f; // How fast the object will move back and forth on the Z-axis

    // Parameters for the random up-and-down effect on the Y-axis
    public float amplitudeY = 0.5f; // How far the object will move up and down on the Y-axis
    public float frequencyY = 1f; // How fast the object will move up and down on the Y-axis

    // Public boolean to mirror Z movement
    public bool mirrorZMovement = false;

    // The starting position of the object
    private Vector3 startPosition;

    // Track the previous position to determine direction for flipping
    private float previousZPosition;

    // Random offset for Y-axis motion to ensure smooth random movement
    private float randomOffset;

    void Start()
    {
        // Record the starting position of the object
        startPosition = transform.position;
        previousZPosition = startPosition.z;
        randomOffset = Random.Range(0f, 2f * Mathf.PI); // Random phase offset for Y-axis motion
    }

    void Update()
    {
        // Calculate the new Z position
        Vector3 newPosition = startPosition;
        float zMovement = Mathf.Sin(Time.time * Mathf.PI * 2 * frequencyZ) * amplitudeZ;
        if (mirrorZMovement)
        {
            zMovement = -zMovement; // Mirror the Z movement if the boolean is true
        }
        newPosition.z += zMovement;

        // Calculate the new Y position with a random smooth movement
        newPosition.y += Mathf.Sin(Time.time * Mathf.PI * 2 * frequencyY + randomOffset) * amplitudeY;

        // Check if the object is turning
        if ((newPosition.z - previousZPosition) * (transform.forward.z) < 0)
        {
            // Flip the direction of the object
            transform.Rotate(0, 180, 0);
        }

        // Apply the new position to the object
        transform.position = newPosition;

        // Update the previous position
        previousZPosition = newPosition.z;
    }
}
