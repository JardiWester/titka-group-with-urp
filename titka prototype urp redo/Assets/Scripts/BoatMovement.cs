using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{

    public float accelerationForce = 10f;
    public float turnSpeed = 100f;

    public CharacterController controller;
    public Interractable Interractable;
    [SerializeField] public GameObject FtoPark;
    private Rigidbody boatRigidbody;

    private void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();
        FtoPark.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BoatPark")
        {
            Debug.Log("Trigger detected with BoatPark!");
            Interractable.canPark = true;
            FtoPark.SetActive(true);
        }
        else
        {
            Debug.Log("park spot name for boat is not set correctly");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "BoatPark")
        {
            Interractable.canPark = false;
        }
        else
        {
            Debug.Log("park spot name for boat is not set correctly");
            FtoPark.SetActive(false);
        }
    }

 

    private void Update()
    {
        if (Interractable != null && Interractable.satDown)
        {
            // Movement
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            Vector3 forwardMovement = -transform.forward * vertical * accelerationForce * Time.deltaTime;
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






