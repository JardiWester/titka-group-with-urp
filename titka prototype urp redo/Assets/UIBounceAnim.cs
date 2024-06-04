using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBounceAnim : MonoBehaviour
{
    // Parameters for the bounce effect
    public float amplitude = 0.5f; // How high the object will bounce
    public float frequency = 1f; // How fast the object will bounce

    // The starting position of the object
    private Vector3 startPosition;

    void Start()
    {
        // Record the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position
        Vector3 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * Mathf.PI * 2 * frequency) * amplitude;

        // Apply the new position to the object
        transform.position = newPosition;
    }
}
