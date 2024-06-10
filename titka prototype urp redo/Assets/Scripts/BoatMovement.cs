using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{

    [SerializeField] private float accelerationForce = 10f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float sprintSpeed = 35f;


    public CharacterController controller;
    public Interractable Interractable;

    private Rigidbody boatRigidbody;

    private void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();

    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BoatPark")
        {
            Debug.Log("Trigger detected with BoatPark!");
            Interractable.canPark = true;

        }
        else
        {
            Debug.Log("park spot name for boat is not set correctly");
        }
    }*/

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "BoatPark")
        {
            Interractable.canPark = false;
        }
        else
        {
            Debug.Log("park spot name for boat is not set correctly");

        }
    }



    private void Update()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? sprintSpeed : accelerationForce;

        if (Interractable != null && Interractable.satDown)
        {
            // Movement
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            Vector3 forwardMovement = -transform.forward * vertical * currentSpeed * Time.deltaTime;
            controller.Move(forwardMovement);

            // Rotation
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                RotateBoat(horizontal);
            }
        }
    }

    private void RotateBoat(float input)
    {
        float rotationAmount = input * turnSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationAmount, 0f);
    }

}






