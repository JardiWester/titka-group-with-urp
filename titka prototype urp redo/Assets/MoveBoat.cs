using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the boat movement
    public float rotationSpeed = 100f; // Speed of the boat rotation
    public Interractable Interractable;
    void Update()
    {
        if (Interractable.satDown)
        {
            // Get input
            float moveInput = Input.GetAxis("Vertical"); // W (1) / S (-1)
            float turnInput = Input.GetAxis("Horizontal"); // A (-1) / D (1)

            // Forward movement
            if (moveInput != 0)
            {
                // Move the boat forward in its local direction
                transform.Translate(-transform.right * moveSpeed * Time.deltaTime * moveInput);
            }

            // Diagonal movement (W+A or W+D)
            if (moveInput != 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                // Calculate diagonal movement by adding forward/backward and sideways movement
                float diagonalSpeed = moveSpeed / Mathf.Sqrt(2); // Adjust speed to maintain the same overall velocity
                transform.Translate(-transform.right * diagonalSpeed * Time.deltaTime * moveInput);
                transform.Translate(transform.forward * diagonalSpeed * Time.deltaTime * turnInput);
            }

            // Turning in place or while moving
            if (turnInput != 0 && moveInput == 0)
            {
                // Rotate the boat
                transform.Rotate(Vector3.up, turnInput * rotationSpeed * Time.deltaTime);
            }
        }
    }

}
