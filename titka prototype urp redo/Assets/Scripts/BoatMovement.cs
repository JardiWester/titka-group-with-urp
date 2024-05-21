using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    
    public float accelerationForce = 10f;
    public float turnSmoothTime = 0.1f;
    public float turnSpeed = 100f; // Added turn speed

    public Transform cameraTransform;
    public CharacterController controller;
    public Interractable Interractable;

    private Rigidbody boatRigidbody;
    private float horizontalInput;

    private void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float vertical = -Input.GetAxisRaw("Vertical");

        if (Interractable != null && Interractable.satDown)
        {
            // Movement
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector3 direction = accelerationForce * vertical * transform.forward;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = targetAngle;    //Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * accelerationForce * Time.deltaTime);
            }

            // Rotation
            horizontalInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                RotateBoat(horizontalInput);
            }
        }
    }

    private void RotateBoat(float input)
    {
        float rotationAmount = input * turnSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        boatRigidbody.MoveRotation(boatRigidbody.rotation * deltaRotation);
    }
}





