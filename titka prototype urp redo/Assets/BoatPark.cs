using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPark : MonoBehaviour
{
    public Transform boatCube;
    public Transform player;
    public Transform ParkHere; //reference to where the boat teleports after player gets off the boat    
    public Transform teleportPlayer; //reference to where the player sits down after interacting
    public Interractable Interractable;


    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ride")
        {
          other.GetComponent<Interractable>().BoatPark = gameObject.GetComponent<BoatPark>();
            Interractable.canPark = true;
        }
        else
        {
            Debug.Log("boat tag set wrongly");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ride")
        {
            Debug.Log("Trigger detected with Boat!");

            Interractable.canPark = false;
            
        }
        else
        {
            Debug.Log("boat tag set wrongly");
        }
    }

    
}
